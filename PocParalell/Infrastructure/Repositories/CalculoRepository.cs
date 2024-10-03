using PocParalell.Domain;
using PocParalell.Infrastructure.Context;
using PocParalell.Infrastructure.Interfaces;

namespace PocParalell.Infrastructure.Repositories
{
    public class CalculoRepository(SqlEntityFrameworkDbContext context) : Repository<PocCalculo>(context), ICalculoRepository
    {
        
    }
}

