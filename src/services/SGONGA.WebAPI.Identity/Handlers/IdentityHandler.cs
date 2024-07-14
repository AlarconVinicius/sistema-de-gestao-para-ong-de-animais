using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SGONGA.Core.Configurations;
using SGONGA.Core.Notifications;
using SGONGA.Core.User;
using SGONGA.WebAPI.Business.Handlers;
using SGONGA.WebAPI.Business.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
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
    private readonly IColaboradorRepository _colaboradorRepository;
    private readonly AppSettings _appSettings;
    public IdentityHandler(INotifier notifier, IAspNetUser appUser, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<AppSettings> appSettings, IColaboradorRepository colaboradorRepository) : base(notifier, appUser)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _roleManager = roleManager;
        _appSettings = appSettings.Value;
        _colaboradorRepository = colaboradorRepository;
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
        var employeeDb = await _colaboradorRepository.GetByIdAsync(request.Id);
        if (employeeDb is null)
        {
            Notify("Colaborador não encontrado.");
            return null!;
        };

        var user = new IdentityUser
        {
            Id = request.Id.ToString(),
            UserName = request.Email,
            Email = request.Email,
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

        await AddOrUpdateUserClaimAsync(new AddOrUpdateUserClaimRequest(request.Id, new UserClaim("Tenant", employeeDb.TenantId.ToString())));

        await _signInManager.SignInAsync(user, false);

        return await GenerateJwt(request.Email);
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
        var userId = request.Id.ToString();
        var userDb = await _userManager.FindByIdAsync(userId);
        if (!await UserExists(userId))
        {
            Notify("Usuário não encontrado.");
            return;
        }

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