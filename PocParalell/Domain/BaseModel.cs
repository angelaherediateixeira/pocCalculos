using System.ComponentModel.DataAnnotations;

namespace PocParalell.Domain
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
    }
}
