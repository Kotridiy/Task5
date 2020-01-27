using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }
        [Range(1, 2000)]
        public decimal? Price { get; set; }
        [Range(0, 1000)]
        public int? Count { get; set; }
    }
}