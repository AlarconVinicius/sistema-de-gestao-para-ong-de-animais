namespace SGONGA.WebAPI.Business.Models.DomainObjects;

public class Contato
{
    public Telefone Telefone { get; set; } = null!;
    public Email Email { get; set; } = null!;

    protected Contato() { }

    public Contato(string telefone, string email)
    {
        if (string.IsNullOrEmpty(telefone) ||
            string.IsNullOrEmpty(email))
            throw new ArgumentNullException("Telefone e Email são obrigatórios.");

        Telefone = new Telefone(telefone);
        Email = new Email(email);
    }

}
