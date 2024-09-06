using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SGONGA.WebAPI.Business.Requests;

public class CreateAnimalRequest : Request
{

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
    [DisplayName("Sexo")]
    public bool Sexo { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Castrado")]
    public bool Castrado { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Cor")]
    public string Cor { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Porte")]
    public string Porte { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Idade")]
    public string Idade { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Descrição")]
    public string Descricao { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Observação")]
    public string Observacao { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Chave Pix")]
    public string ChavePix { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [MinLength(1, ErrorMessage = "O campo {0} deve ter no mínimo {1} fotos.")]
    [DisplayName("Foto")]
    public string Foto { get; set; } = string.Empty;

    public CreateAnimalRequest() { }

    public CreateAnimalRequest(string nome, string especie, string raca, bool sexo, bool castrado, string cor, string porte, string idade, string descricao, string observacao, string foto, string chavePix = "")
    {
        Nome = nome;
        Especie = especie;
        Raca = raca;
        Sexo = sexo;
        Castrado = castrado;
        Cor = cor;
        Porte = porte;
        Idade = idade;
        Descricao = descricao;
        Observacao = observacao;
        ChavePix = chavePix;
        Foto = foto;
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
    [DisplayName("Sexo")]
    public bool Sexo { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [DisplayName("Castrado")]
    public bool Castrado { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Cor")]
    public string Cor { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Porte")]
    public string Porte { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Idade")]
    public string Idade { get; set; } = string.Empty;

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
    [DisplayName("Foto")]
    public string Foto { get; set; } = string.Empty;

    public UpdateAnimalRequest() { }

    public UpdateAnimalRequest(Guid id, string nome, string especie, string raca, bool sexo, bool castrado, string cor, string porte, string idade, string descricao, string observacao, string foto, string chavePix = "")
    {
        Id = id;
        Nome = nome;
        Especie = especie;
        Raca = raca;
        Sexo = sexo;
        Castrado = castrado;
        Cor = cor;
        Porte = porte;
        Idade = idade;
        Descricao = descricao;
        Observacao = observacao;
        ChavePix = chavePix;
        Foto = foto;
    }
}

public class GetAllAnimaisRequest : PagedRequest
{
    public bool TenantFiltro { get; set; }
    public GetAllAnimaisRequest() { }

    public GetAllAnimaisRequest(int pageSize = 50, int pageNumber = 1, string? query = null, bool returnAll = false, bool tenantFiltro = false) : base(pageSize, pageNumber, query, returnAll)
    {
        TenantFiltro = tenantFiltro;
    }
}

public class GetAnimalByIdRequest : Request
{
    public bool TenantFiltro { get; set; }
    public Guid Id { get; set; }

    public GetAnimalByIdRequest() { }

    public GetAnimalByIdRequest(Guid id, bool tenantFiltro = false)
    {
        Id = id;
        TenantFiltro = tenantFiltro;
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
