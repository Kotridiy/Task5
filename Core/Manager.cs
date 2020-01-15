using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core
{
    public class Manager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int? Age { get; set; }
        public decimal? Salary { get; set; }

        [NotMapped]
        public IEnumerable<SoldProduct> SoldProducts { get; set; }
    }
}