using BLL.DTO;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class SoldProductViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public ClientDTO Client { get; set; }
        public ManagerDTO Manager { get; set; }
        public ProductDTO Product { get; set; }
    }
}