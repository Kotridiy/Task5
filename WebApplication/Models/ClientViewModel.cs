using Core;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }
        public Gender Gender { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Birthday { get; set; }
    }
}