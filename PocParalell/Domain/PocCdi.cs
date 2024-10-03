using System.ComponentModel.DataAnnotations;

namespace PocParalell.Domain
{
    public class PocCdi : BaseModel
    {
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
    }
}
