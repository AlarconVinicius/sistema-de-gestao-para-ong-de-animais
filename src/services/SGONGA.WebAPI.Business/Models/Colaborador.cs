using SGONGA.WebAPI.Business.Models.DomainObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGONGA.WebAPI.Business.Models;

public class Colaborador : Entity
{
    public Guid TenantId { get; private set; }
    public Email Email { get; private set; } = null!;


    [ForeignKey(nameof(TenantId))]
    public virtual ONG? ONG { get; private set; }

    public Colaborador() { }

    public Colaborador(Guid id, string email) : base(id)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email não pode ser nulo ou vazio.");

        Email = new Email(email);
    }

    public Colaborador(Guid id, Guid tenantId, string email) : base(id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id não pode ser vazio.");
        if (tenantId == Guid.Empty)
            throw new ArgumentException("TenantId não pode ser vazio.");
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email não pode ser nulo ou vazio.");

        TenantId = tenantId;
        Email = new Email(email);
    }

    #region Setters
    public void SetEmail(string email)
    {
        Email = new Email(email);
    }
    #endregion
}
