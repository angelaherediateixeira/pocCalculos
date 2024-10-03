namespace PocParalell.Services.Interfaces
{
    public interface ICalculoService
    {
        Task InsereUsandoForeach();
        Task InsereUsandoPaginacaoBulk();
        Task InsereUsandoTasks();
    }
}
