using Core;
using DAL.Interfaces;

namespace DAL.DataModels
{
    public class Manager : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public decimal Salary { get; set; }
    }
}