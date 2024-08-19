using SGONGA.WebAPI.Business.Models.DomainObjects;

namespace SGONGA.WebAPI.Business.Models;
public class SolicitacaoCadastro : Entity
{
    //public string Nome { get; private set; } = string.Empty;
    //public string Instagram { get; private set; } = string.Empty;
    //public string Documento { get; private set; } = string.Empty;
    //public Contato Contato { get; private set; } = null!;
    //public Endereco Endereco { get; private set; } = null!;
    //public string? ChavePix { get; set; } = string.Empty;
    //public string Nome { get; private set; } = string.Empty;
    //public string Documento { get; private set; } = string.Empty;
    //public Contato Contato { get; private set; } = null!;
    //public DateTime DataNascimento { get; private set; }
}
public sealed class SolicitacaoCadastroProvider
{
    public Guid OngId { get; set; }
    public Guid ColaboradorId { get; set; }
}