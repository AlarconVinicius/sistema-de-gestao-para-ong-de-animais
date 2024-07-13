using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGONGA.WebAPI.Business.Requests;

public class CreateAnimalRequest : Request
{
    [DisplayName("Tenant Id")]
    public Guid TenantId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Nome")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Espécie")]
    public string Especie { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Raça")]
    public string Raca { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Cor")]
    public string Cor { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Porte")]
    public string Porte { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Descrição")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Observação")]
    public string Observacao { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Chave Pix")]
    public string ChavePix { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [MinLength(1, ErrorMessage = "O campo {0} deve ter no mínimo {1} fotos.")]
    [DisplayName("Fotos")]
    public List<string> Fotos { get; set; } = new List<string>();

    public CreateAnimalRequest() { }

    public CreateAnimalRequest(string nome, string especie, string raca, string cor, string porte, string descricao, string observacao, List<string> fotos, string chavePix = "")
    {
        Nome = nome;
        Especie = especie;
        Raca = raca;
        Cor = cor;
        Porte = porte;
        Descricao = descricao;
        Observacao = observacao;
        ChavePix = chavePix;
        Fotos = fotos;
    }
}

public class UpdateAnimalRequest : Request
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Id")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Nome")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Espécie")]
    public string Especie { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Raça")]
    public string Raca { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Cor")]
    public string Cor { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Porte")]
    public string Porte { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Descrição")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Observação")]
    public string Observacao { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Chave Pix")]
    public string ChavePix { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [MinLength(1, ErrorMessage = "O campo {0} deve ter no mínimo {1} fotos.")]
    [DisplayName("Fotos")]
    public List<string> Fotos { get; set; } = new List<string>();

    public UpdateAnimalRequest() { }

    public UpdateAnimalRequest(Guid id, string nome, string especie, string raca, string cor, string porte, string descricao, string observacao, List<string> fotos, string chavePix = "")
    {
        Id = id;
        Nome = nome;
        Especie = especie;
        Raca = raca;
        Cor = cor;
        Porte = porte;
        Descricao = descricao;
        Observacao = observacao;
        ChavePix = chavePix;
        Fotos = fotos;
    }
}

public class GetAllAnimaisRequest : PagedRequest
{
    public GetAllAnimaisRequest() { }

    public GetAllAnimaisRequest(int pageSize = 50, int pageNumber = 1, string? query = null, bool returnAll = false) : base(pageSize, pageNumber, query, returnAll)
    {

    }
}

public class GetAnimalByIdRequest : Request
{
    public Guid Id { get; set; }

    public GetAnimalByIdRequest() { }

    public GetAnimalByIdRequest(Guid id)
    {
        Id = id;
    }
}

public class DeleteAnimalRequest : Request
{
    public Guid Id { get; set; }

    public DeleteAnimalRequest() { }

    public DeleteAnimalRequest(Guid id)
    {
        Id = id;
    }
}
