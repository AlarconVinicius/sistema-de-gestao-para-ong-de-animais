using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SGONGA.WebAPI.Business.Responses;

public class AnimalResponse : Response
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Id")]
    public Guid Id { get; set; }

    [DisplayName("Tenant Id")]
    public Guid TenantId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Nome")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Espécie")]
    public string Especie { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Raça")]
    public string Raca { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Cor")]
    public string Cor { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Porte")]
    public string Porte { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Descrição")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Observação")]
    public string Observacao { get; set; } = string.Empty;

    [DisplayName("Chave Pix")]
    public string ChavePix { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Fotos")]
    public List<string> Fotos { get; set; } = new List<string>();

    [DisplayName("Data de Cadastro")]
    public DateTime CreatedAt { get; set; }

    [DisplayName("Data de Modificação")]
    public DateTime UpdatedAt { get; set; }

    public AnimalResponse() { }

    public AnimalResponse(Guid id, Guid tenantId, string nome, string especie, string raca, string cor, string porte, string descricao, string observacao, string chavePix, List<string> fotos, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        TenantId = tenantId;
        Nome = nome;
        Especie = especie;
        Raca = raca;
        Cor = cor;
        Porte = porte;
        Descricao = descricao;
        Observacao = observacao;
        ChavePix = chavePix;
        Fotos = fotos;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}
