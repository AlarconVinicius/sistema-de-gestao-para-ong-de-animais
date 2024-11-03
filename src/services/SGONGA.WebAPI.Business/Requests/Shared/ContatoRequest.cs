using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using FluentValidation;

namespace SGONGA.WebAPI.Business.Requests.Shared;
public class ContatoRequest
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [Phone(ErrorMessage = "O campo {0} não é válido.")]
    [StringLength(15, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("Whatsapp")]
    public string Telefone { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo {0} não é válido.")]
    [StringLength(254, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
    [DisplayName("E-mail")]
    public string Email { get; set; } = string.Empty;

    public ContatoRequest() { }

    public ContatoRequest(string telefone, string email)
    {
        Telefone = telefone;
        Email = email;
    }
    public class ContatoRequestValidator : AbstractValidator<ContatoRequest>
    {
        public ContatoRequestValidator()
        {
            RuleFor(c => c.Telefone)
                .NotEmpty().WithMessage("O telefone é obrigatório.")
                .MaximumLength(15).WithMessage("O telefone deve ter no máximo {0} caracteres.")
                .Matches(@"(\(?\d{2}\)?\s)?(\d{4,5}\-\d{4})").WithMessage("O telefone não é válido.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O e-mail não é válido.")
                .MaximumLength(254).WithMessage("O e-mail deve ter no máximo {0} caracteres.");
        }
    }
}
