namespace PocParalell.Domain
{
    public class PocCalculo : BaseModel
    {
        public int IdPosicao { get; set; }
        public int TipoCalculo { get; set; }
        public string Conta { get; set; } = string.Empty;
        public decimal Valor { get; set; }
    }
}
