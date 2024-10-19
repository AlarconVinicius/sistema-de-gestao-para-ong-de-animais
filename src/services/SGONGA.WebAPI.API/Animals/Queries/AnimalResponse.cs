namespace SGONGA.WebAPI.API.Animals.Queries;

public record AnimalResponse(
    Guid Id, 
    Guid OngId, 
    string Nome, 
    string Especie, 
    string Raca, 
    bool Sexo, 
    bool Castrado, 
    string Cor, 
    string Porte, 
    string Idade, 
    string Ong, 
    string Endereco, 
    string Descricao, 
    string Observacao, 
    string ChavePix, 
    string Foto, 
    DateTime CreatedAt, 
    DateTime UpdatedAt);