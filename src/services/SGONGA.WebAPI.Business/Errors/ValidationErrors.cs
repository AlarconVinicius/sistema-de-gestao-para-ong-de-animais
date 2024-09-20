using SGONGA.WebAPI.Business.Abstractions;

namespace SGONGA.WebAPI.Business.Errors;

public static class ValidationErrors
{
    public static Error EmailEmUso(string email) => Error.Conflict("EMAIL_EM_USO", $"O email = '{email}' já está em uso.");
    public static Error ApelidoEmUso(string apelido) => Error.Conflict("APELIDO_EM_USO", $"O apelido = '{apelido}' já está em uso.");
    public static Error DocumentoEmUso(string documento) => Error.Conflict("DOCUMENTO_EM_USO", $"O documento = '{documento}' já está em uso.");
}