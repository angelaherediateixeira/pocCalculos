using PocParalell.Domain;
using PocParalell.Infrastructure.Context;
using PocParalell.Infrastructure.Interfaces;

namespace PocParalell.Infrastructure.Repositories
{
    public class CdiRepository(SqlEntityFrameworkDbContext context) : Repository<PocCdi>(context), ICdiRepository
    {
    }
}
