using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.DataModels
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int? Count { get; set; }
        [NotMapped]
        public IEnumerable<SoldProduct> SoldProducts { get; set; }
    }
}