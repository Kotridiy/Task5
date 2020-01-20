using Core;
using DAL.DataModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL.EntityFramework
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SoldProduct> SoldProducts { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            if (!Database.EnsureCreated())
            {
                Clients.AddRange(
                    new Client() { Name = "Даша Мартыненко", Birthday = new DateTime(1997, 3, 10), Gender = Gender.Female },
                    new Client() { Name = "Егор Земов", Birthday = new DateTime(1985, 4, 15), Gender = Gender.Male },
                    new Client() { Name = "Димка Димка", Birthday = new DateTime(2005, 7, 1), Gender = Gender.Male },
                    new Client() { Name = "Пушок", Birthday = DateTime.Today, Gender = Gender.Undetermined }
                );

                Managers.AddRange(
                    new Manager() { Name = "Иван Требушко", Age = 23, Salary = 1006, Gender = Gender.Male },
                    new Manager() { Name = "Вероника Ковальчук", Age = 22, Salary = 1765, Gender = Gender.Female }
                );

                Products.AddRange(
                    new Product() { Name = "Вилы", Count = 5, Price = 23},
                    new Product() { Name = "Грабли", Count = 13, Price = 15},
                    new Product() { Name = "Галоши", Count = 20, Price = 20}
                );
            }
        }
    }
}