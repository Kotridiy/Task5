using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core
{
    public class Client
    {
        public int Id { get; set; }
        public Name Name { get; set; }
        public Sex Sex { get; set; }
        public DateTime Birthday { get; set; }
        [NotMapped]
        public IEnumerable<SoldProduct> BoughtProducts { get; set; }
    }
}