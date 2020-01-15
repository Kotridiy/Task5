using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public static class DataAccessBuilder
    {
        public static IUnitOfWork CreateUnitOfWork(string info = "")
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>().UseSqlServer(info).Options;

            ApplicationContext context = new ApplicationContext(options);

            return new DataUnitOfWork(context);
        }
    }
}
