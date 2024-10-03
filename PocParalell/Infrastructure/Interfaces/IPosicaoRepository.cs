using PocParalell.Domain;

namespace PocParalell.Infrastructure.Interfaces
{
    public interface IPosicaoRepository : IRepository<PocPosicao>
    {
        int TotalPosicoes();
        IEnumerable<PocPosicao> ObterPosicoes(int page, int totalRecords);
    }
}
