using Core;
using DAL.Interfaces;
using System;

namespace DAL.DataModels
{
    public class Client : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthday { get; set; }
    }
}