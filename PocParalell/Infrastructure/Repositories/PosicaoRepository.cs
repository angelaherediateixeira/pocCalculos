using PocParalell.Domain;
using PocParalell.Infrastructure.Context;
using PocParalell.Infrastructure.Interfaces;
using System.Data.Entity;

namespace PocParalell.Infrastructure.Repositories
{
    public class PosicaoRepository(SqlEntityFrameworkDbContext context) : Repository<PocPosicao>(context), IPosicaoRepository
    {
        public int TotalPosicoes()
        {
            return context.Posicoes.Count();
        }

        public IEnumerable<PocPosicao> ObterPosicoes(int page, int totalRecords)
        {
            return context.Posicoes.Skip(totalRecords * page).Take(totalRecords).OrderBy(x => x.Id);
        }
    }
}
