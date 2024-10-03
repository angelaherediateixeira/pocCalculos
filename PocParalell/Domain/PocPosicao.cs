using System.ComponentModel.DataAnnotations;

namespace PocParalell.Domain
{
    public class PocPosicao : BaseModel
    {
        public string Conta { get; set; }
        public decimal Valor { get; set; }
        public int Quantidade {  get; set; }
        public decimal ValorCalculado { get; set; }
        public DateTime Data {  get; set; }
    }
}
