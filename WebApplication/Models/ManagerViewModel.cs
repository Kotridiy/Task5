using Core;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class ManagerViewModel
    {
        public int Id { get; set; }
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }
        public Gender Gender { get; set; }
        [Range(17, 70)]
        public int? Age { get; set; }
        [Range(0, 10000)]
        public decimal? Salary { get; set; }
    }
}