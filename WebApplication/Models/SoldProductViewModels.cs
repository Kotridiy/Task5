using BLL.DTO;
using Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class CreateSoldProductViewModel
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Required]
        public int ClientId { get; set; }
        [Required]
        public int ProductId { get; set; }
    }

    public class DetailSoldProductViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public ClientDTO Client { get; set; }
        public ManagerDTO Manager { get; set; }
        public ProductDTO Product { get; set; }
    }
}