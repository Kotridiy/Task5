using System;

namespace BLL.DTO
{
    public class SoldProductDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public ClientDTO Client { get; set; }
        public ManagerDTO Manager { get; set; }
        public ProductDTO Product { get; set; }
    }
}
