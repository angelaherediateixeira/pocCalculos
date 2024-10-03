using PocParalell.Domain;
using PocParalell.Infrastructure.Context;
using PocParalell.Infrastructure.Interfaces;

namespace PocParalell.Infrastructure.Repositories
{
    public class ExecucaoRepository(SqlEntityFrameworkDbContext context) : Repository<PocExecucao>(context), IExecucaoRepository
    {
    }
}
