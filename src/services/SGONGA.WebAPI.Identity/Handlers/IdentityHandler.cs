using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SGONGA.Core.Configurations;
using SGONGA.Core.Notifications;
using SGONGA.Core.User;
using SGONGA.WebAPI.Business.Handlers;
using SGONGA.WebAPI.Business.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Requests;
using SGONGA.WebAPI.Business.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SGONGA.WebAPI.Identity.Handlers;

public class IdentityHandler : BaseHandler, IIdentityHandler
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IONGHandler _ongHandler;
    private readonly IAdotanteHandler _adotanteHandler;
    private readonly AppSettings _appSettings;
    public IdentityHandler(INotifier notifier, IAspNetUser appUser, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<AppSettings> appSettings, IUnitOfWork unitOfWork, IONGHandler ongHandler, IAdotanteHandler adotanteHandler) : base(notifier, appUser)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _appSettings = appSettings.Value;
        _unitOfWork = unitOfWork;
        _ongHandler = ongHandler;
        _adotanteHandler = adotanteHandler;
    }

    public async Task<LoginUserResponse> LoginAsync(LoginUserRequest request)
    {
        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Senha, false, true);

        if (result.Succeeded)
            return await GenerateJwt(request.Email);

        if (result.IsLockedOut)
        {
            Notify("Usuário temporariamente bloqueado devido às tentativas inválidas.");
            return null!;
        }

        Notify("Usuário ou senha inválidos.");
        return null!;
    }

    public async Task Logout()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<LoginUserResponse> CreateAsync(CreateUserRequest request)
    {
        if (request.Senha != request.ConfirmarSenha)
        {
            Notify("As senhas não conferem.");
            return null!;
        }
        if (!await ManipularCriacaoUsuarioAsync(request.Usuario))
        {
            Notify("Dados de usuário inválidos.");
            return null!;
        }
        var user = new IdentityUser
        {
            Id = request.Usuario.Id.ToString(),
            UserName = request.Usuario.Contato.Email,
            Email = request.Usuario.Contato.Email,
            EmailConfirmed = true,
        };

        var result = await _userManager.CreateAsync(user, request.Senha);

        if (!result.Succeeded)
        {
            foreach (var errors in result.Errors)
            {
                Notify(errors.Description);
            }
            return null!;
        }
        await AddOrUpdateUserClaimAsync(new AddOrUpdateUserClaimRequest(request.Usuario.Id, new UserClaim("Tenant", request.Usuario.TenantId.ToString())));
        await _unitOfWork.CommitAsync();

        await _signInManager.SignInAsync(user, false);

        return await GenerateJwt(request.Usuario.Contato.Email);
    }

    public async Task UpdateEmailAsync(UpdateUserEmailRequest request)
    {
        var userId = request.Id.ToString();
        var userDb = await _userManager.FindByIdAsync(userId);
        if (!await UserExists(userId))
        {
            Notify("Usuário não encontrado.");
            return;
        }
        userDb!.UserName = request.NovoEmail;
        userDb.NormalizedUserName = request.NovoEmail.ToUpper();
        userDb.Email = request.NovoEmail;
        userDb.NormalizedEmail = request.NovoEmail.ToUpper();
        await _userManager.UpdateAsync(userDb);
        return;
    }

    public async Task UpdatePasswordAsync(UpdateUserPasswordRequest request)
    {
        string userId = request.Id.ToString();
        var userDb = await _userManager.FindByIdAsync(userId);
        if (!await UserExists(userId))
        {
            Notify("Usuário não encontrado.");
            return;
        }

        var passwordCheckResult = await _userManager.CheckPasswordAsync(userDb!, request.SenhaAntiga);

        if (!passwordCheckResult)
        {
            Notify("A senha atual está incorreta.");
            return;
        }

        if (request.NovaSenha != request.ConfirmarNovaSenha)
        {
            Notify("As senhas não conferem.");
            return;
        }

        var updatePasswordResult = await _userManager.ChangePasswordAsync(userDb!, request.SenhaAntiga, request.NovaSenha);

        if (!updatePasswordResult.Succeeded)
        {
            foreach (var error in updatePasswordResult.Errors)
            {
                Notify(error.Description);
            }
            return;
        }

        return;
    }

    public async Task DeleteAsync(DeleteUserRequest request)
    {
        var userId = request.Id;
        if(AppUser.GetUserId() != userId && !EhSuperAdmin())
        {
            Notify("Você não tem permissão para deletar.");
            return;
        }
        var usuarioTipo = IdentificarTipoUsuario(userId);
        if (usuarioTipo is null)
        {
            Notify("Usuário não encontrado.");
            return;
        }
        if (!await UserExists(userId.ToString()))
        {
            Notify("Usuário não encontrado.");
            return;
        }
        if (!await ManipularDelecaoUsuarioAsync(request, (EUsuarioTipo)usuarioTipo))
        {
            Notify("Dados de usuário inválidos.");
            return;
        }
        var userDb = await _userManager.FindByIdAsync(userId.ToString());

        var logins = await _userManager.GetLoginsAsync(userDb!);
        foreach (var login in logins)
        {
            await _userManager.RemoveLoginAsync(userDb!, login.LoginProvider, login.ProviderKey);
        }

        var claims = await _userManager.GetClaimsAsync(userDb!);
        foreach (var claim in claims)
        {
            await _userManager.RemoveClaimAsync(userDb!, claim);
        }

        var roles = await _userManager.GetRolesAsync(userDb!);
        foreach (var role in roles)
        {
            await _userManager.RemoveFromRoleAsync(userDb!, role);
        }

        var result = await _userManager.DeleteAsync(userDb!);
        if (!result.Succeeded)
        {
            Notify("Erro ao deletar usuário.");
            return;
        }
        await _unitOfWork.CommitAsync();
        return;
    }

    public async Task<UserResponse> GetByIdAsync(GetUserByIdRequest request)
    {
        var userId = request.Id.ToString();
        var userDb = await _userManager.FindByIdAsync(userId);
        if (!await UserExists(userId))
        {
            Notify("Usuário não encontrado.");
            return null!;
        }
        return new UserResponse(Guid.Parse(userDb!.Id), userDb.Email!);
    }

    public Task<PagedResponse<UserResponse>> GetAllAsync(GetAllUsersRequest request)
    {
        throw new NotImplementedException();
    }

    private bool EhSuperAdmin()
    {
        if (AppUser.HasClaim("Permissions", "SuperAdmin"))
        {
            return true;
        }
        return false;
    }
    private EUsuarioTipo? IdentificarTipoUsuario(Guid id)
    {
        if (_unitOfWork.AdotanteRepository.SearchAsync(f => f.Id == id).Result.Any())
        {
            return EUsuarioTipo.Adotante;
        }
        if (_unitOfWork.ONGRepository.SearchAsync(f => f.Id == id).Result.Any())
        {
            return EUsuarioTipo.ONG;
        }
        return null!;
    }
    private async Task<bool> ManipularCriacaoUsuarioAsync(CreateUsuarioRequest request)
    {
        Usuario? userDb;

        switch (request.UsuarioTipo)
        {
            case EUsuarioTipo.Adotante:
                userDb = await _unitOfWork.AdotanteRepository.GetByIdWithoutTenantAsync(request.Id);
                if (userDb == null)
                {
                    CreateAdotanteRequest adotante = new(request.Id, request.TenantId, request.Nome, request.Apelido, request.Documento, request.Site, request.Contato, request.TelefoneVisivel, request.AssinarNewsletter, request.DataNascimento, request.Estado, request.Cidade, request.Sobre);
                    await _adotanteHandler.CreateAsync(adotante);
                }
                break;

            case EUsuarioTipo.ONG:
                userDb = await _unitOfWork.ONGRepository.GetByIdWithoutTenantAsync(request.Id);
                if (userDb == null)
                {
                    CreateONGRequest ong = new(request.Id, request.TenantId, request.Nome, request.Apelido, request.Documento, request.Site, request.Contato, request.TelefoneVisivel, request.AssinarNewsletter, request.DataNascimento, request.Estado, request.Cidade, request.Sobre, "");
                    await _ongHandler.CreateAsync(ong);
                }
                break;
            default:
                return false;
        }
        if (!IsOperationValid())
        {
            return false;
        };
        return true;
    }
    private async Task<bool> ManipularDelecaoUsuarioAsync(DeleteUserRequest request, EUsuarioTipo usuarioTipo)
    {
        switch (usuarioTipo)
        {
            case EUsuarioTipo.Adotante:
                    await _adotanteHandler.DeleteAsync(new DeleteAdotanteRequest(request.Id));
                break;

            case EUsuarioTipo.ONG:
                    await _ongHandler.DeleteAsync(new DeleteONGRequest(request.Id));
                break;
            default:
                return false;
        }
        if (!IsOperationValid())
        {
            return false;
        };
        return true;
    }
    #region IdentityHelpers
    private async Task AddOrUpdateUserClaimAsync(AddOrUpdateUserClaimRequest request)
    {
        var userId = request.Id.ToString();
        if (!await UserExists(userId))
        {
            Notify("Usuário não encontrado.");
            return;
        }
        var userDb = await _userManager.FindByIdAsync(userId);
        var existingClaims = await _userManager.GetClaimsAsync(userDb!);
        Claim existingClaim;
        existingClaim = existingClaims.FirstOrDefault(c => c.Type == request.NewClaim.Type)!;
        if (existingClaim != null)
        {
            await _userManager.RemoveClaimAsync(userDb!, existingClaim);
        }
        var newClaim = new Claim(request.NewClaim.Type, request.NewClaim.Value);
        await _userManager.AddClaimAsync(userDb!, newClaim);
        return;
    }
    private async Task<bool> UserExists(string userId)
    {
        return await _userManager.FindByIdAsync(userId) != null!;
    }
    #endregion

    #region JwtHelpers
    private async Task<LoginUserResponse> GenerateJwt(string email)
    {
        var userDb = await _userManager.FindByEmailAsync(email);
        if (userDb == null)
        {
            return null!;
        }
        var claims = (await _userManager.GetClaimsAsync(userDb)).ToList();
        var userRoles = await _userManager.GetRolesAsync(userDb);
        AddStandardClaims(claims, userDb);
        AddUserRolesClaims(claims, userRoles);
        var token = GenerateToken(claims);
        var response = CreateResponse(token, userDb, claims);
        return response;
    }
    private void AddStandardClaims(List<Claim> claims, IdentityUser user)
    {
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email!));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
    }
    private void AddUserRolesClaims(List<Claim> claims, IList<string> userRoles)
    {
        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim("role", userRole));
        }
    }
    private SecurityToken GenerateToken(List<Claim> claims)
    {
        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = identityClaims,
            Issuer = _appSettings.Issuer,
            Audience = _appSettings.Audience,
            Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });
    }
    private LoginUserResponse CreateResponse(SecurityToken token, IdentityUser user, List<Claim> claims)
    {
        var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return new LoginUserResponse
        {
            AccessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds,
            UserToken = new UserToken
            {
                Id = user.Id,
                Email = user.Email!,
                Claims = claims.Select(c => new UserClaim(c.Type, c.Value))
            }
        };
    }
    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - DateTimeOffset.UnixEpoch).TotalSeconds);

    #endregion

}