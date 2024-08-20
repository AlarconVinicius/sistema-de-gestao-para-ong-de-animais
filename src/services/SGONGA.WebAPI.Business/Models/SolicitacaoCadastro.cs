using SGONGA.WebAPI.Business.Models.DomainObjects;

namespace SGONGA.WebAPI.Business.Models;
public class SolicitacaoCadastro : Entity
{
    public Guid IdOng { get; private set; }
    public string NomeOng { get; private set; } = string.Empty;
    public string InstagramOng { get; private set; } = string.Empty;
    public string DocumentoOng { get; private set; } = string.Empty;
    public Contato ContatoOng { get; private set; } = null!;
    public Endereco EnderecoOng { get; private set; } = null!;
    public string? ChavePixOng { get; private set; } = string.Empty;
    public Guid IdResponsavel { get; private set; }
    public string NomeResponsavel { get; private set; } = string.Empty;
    public string DocumentoResponsavel { get; private set; } = string.Empty;
    public Contato ContatoResponsavel { get; private set; } = null!;
    public DateTime DataNascimentoResponsavel { get; private set; }

    public SolicitacaoCadastro() { }

    public SolicitacaoCadastro(Guid idOng, string nomeOng, string instagramOng, string documentoOng, Contato contatoOng, Endereco enderecoOng, string? chavePixOng, Guid idResponsavel, string nomeResponsavel, string documentoResponsavel, Contato contatoResponsavel, DateTime dataNascimentoResponsavel)
    {
        IdOng = idOng;
        NomeOng = nomeOng;
        InstagramOng = instagramOng;
        DocumentoOng = documentoOng;
        ContatoOng = contatoOng;
        EnderecoOng = enderecoOng;
        ChavePixOng = chavePixOng;
        NomeResponsavel = nomeResponsavel;
        DocumentoResponsavel = documentoResponsavel;
        ContatoResponsavel = contatoResponsavel;
        DataNascimentoResponsavel = dataNascimentoResponsavel;
    }

    public string GeneratePassword()
    {
        if (string.IsNullOrWhiteSpace(DocumentoResponsavel))
            throw new ArgumentException("Documento do Responsavel não pode ser nulo ou vazio.");

        string primeirosDigitosDocumento = DocumentoResponsavel.Length >= 4 ? DocumentoResponsavel.Substring(0, 4) : DocumentoResponsavel;

        string anoNascimento = DataNascimentoResponsavel.Year.ToString();

        string senhaGerada = $"{primeirosDigitosDocumento}@{anoNascimento}";

        return senhaGerada;
    }

}

public sealed class SolicitacaoCadastroProvider
{
    public Guid OngId { get; set; }
    public Guid ColaboradorId { get; set; }
}