using Core;
using System;

namespace DAL.DataModels
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthday { get; set; }
    }
}