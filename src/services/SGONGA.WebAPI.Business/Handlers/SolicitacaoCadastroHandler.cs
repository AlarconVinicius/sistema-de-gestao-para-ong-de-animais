using SGONGA.Core.Notifications;
using SGONGA.Core.User;
using SGONGA.WebAPI.Business.Interfaces.Handlers;
using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Business.Requests;

namespace SGONGA.WebAPI.Business.Handlers;
public class SolicitacaoCadastroHandler : BaseHandler//, IAnimalHandler
{
    public readonly IUnitOfWork _unitOfWork;
    public readonly IONGHandler _ongHandler;
    public readonly IColaboradorHandler _colaboradorHandler;
    public readonly IIdentityHandler _identityHandler;
    public readonly SolicitacaoCadastroProvider _solicitacaoCadastroProvider;

    public SolicitacaoCadastroHandler(INotifier notifier, IAspNetUser appUser, IUnitOfWork unitOfWork, IONGHandler ongHandler, IColaboradorHandler colaboradorHandler, IIdentityHandler identityHandler, SolicitacaoCadastroProvider solicitacaoCadastroProvider) : base(notifier, appUser)
    {
        _unitOfWork = unitOfWork;
        _ongHandler = ongHandler;
        _colaboradorHandler = colaboradorHandler;
        _identityHandler = identityHandler;
        _solicitacaoCadastroProvider = solicitacaoCadastroProvider;
    }

    public Task RequestRegistration(RequestCreateUserRequest request)
    {
        throw new NotImplementedException();
    }
    public async Task ApproveRegistration(RequestCreateUserRequest request)
    {
        try
        {
            //Cadastrar ONG
            await _ongHandler.CreateAsync(new CreateONGRequest(request.ONG.Nome, request.ONG.Instagram, request.ONG.Documento, request.ONG.ChavePix, request.ONG.Contato, request.ONG.Endereco));
            if (!IsOperationValid())
            {
                return;
            };

            //Cadastrar Colaborador
            Guid ongId = _solicitacaoCadastroProvider.OngId;
            await _colaboradorHandler.CreateAsync(new CreateColaboradorRequest(request.Responsavel.Nome, request.Responsavel.Documento, request.Responsavel.DataNascimento, request.Responsavel.Contato), ongId);
            if (!IsOperationValid())
            {
                await _ongHandler.DeleteAsync(new DeleteONGRequest(ongId));
                return;
            };
            //Cadastrar Login
            Guid colaboradorId = _solicitacaoCadastroProvider.ColaboradorId;
            string senha = "Senha@123";
            var result = await _identityHandler.CreateAsync(new CreateUserRequest(colaboradorId, request.Responsavel.Contato.Email, senha, senha));
            if (!IsOperationValid())
            {
                await _colaboradorHandler.DeleteAsync(new DeleteColaboradorRequest(colaboradorId));
                await _ongHandler.DeleteAsync(new DeleteONGRequest(ongId));
                return;
            };
            //Mudar status para Aprovado
            //Enviar email para o usuário informando a senha default
            return;
        }
        catch
        {
            Notify("Não foi possível concluir a ação.");
            return;
        }
    }

    public Task DenyRegistration(RequestCreateUserRequest request)
    {
        throw new NotImplementedException();
    }
}
