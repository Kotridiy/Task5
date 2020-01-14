using System;

namespace Core
{
    public class SoldProduct
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Client Client { get; set; }
        public Manager Manager { get; set; }
        public Product Product { get; set; }
    }
}
