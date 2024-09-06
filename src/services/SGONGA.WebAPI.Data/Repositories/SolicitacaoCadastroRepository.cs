using SGONGA.WebAPI.Business.Interfaces.Repositories;
using SGONGA.WebAPI.Business.Models;
using SGONGA.WebAPI.Data.Context;

namespace SGONGA.WebAPI.Data.Repositories;
internal class SolicitacaoCadastroRepository : Repository<SolicitacaoCadastro>, ISolicitacaoCadastroRepository
{
    public SolicitacaoCadastroRepository(ONGDbContext db) : base(db)
    {
    }

}