using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGONGA.WebAPI.Business.Responses;

public class AnimalResponse : Response
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Id")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("ONG Id")]
    public Guid ONGId { get; set; }

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
    [DisplayName("Sexo")]
    public bool Sexo { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Castrado")]
    public bool Castrado { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Cor")]
    public string Cor { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Porte")]
    public string Porte { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Idade")]
    public string Idade { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Descrição")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("ONG")]
    public string ONG { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Endereço")]
    public string Endereco { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Observação")]
    public string Observacao { get; set; } = string.Empty;

    [DisplayName("Chave Pix")]
    public string ChavePix { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Foto")]
    public string Foto { get; set; } = string.Empty;

    [DisplayName("Data de Cadastro")]
    public DateTime CreatedAt { get; set; }

    [DisplayName("Data de Modificação")]
    public DateTime UpdatedAt { get; set; }

    public AnimalResponse() { }

    public AnimalResponse(Guid id, Guid ongId, string nome, string especie, string raca, bool sexo, bool castrado, string cor, string porte, string idade, string ong, string endereco, string descricao,  string observacao, string chavePix, string foto, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        ONGId = ongId;
        Nome = nome;
        Especie = especie;
        Raca = raca;
        Sexo = sexo;
        Castrado = castrado;
        Cor = cor;
        Porte = porte;
        Idade = idade;
        Descricao = descricao;
        ONG = ong;
        Endereco = endereco;
        Observacao = observacao;
        ChavePix = chavePix;
        Foto = foto;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }
}
