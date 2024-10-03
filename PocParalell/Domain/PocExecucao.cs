namespace PocParalell.Domain
{
    public class PocExecucao : BaseModel
    {
        public int TipoCalculo { get; set; }
        public string Tempo { get; set; } = string.Empty;
        public DateTime Data {  get; set; }
    }
}
