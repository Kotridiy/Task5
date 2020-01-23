using DAL.EntityFramework;
using DAL.Interfaces;
using DAL.Implements;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace DAL
{
    public static class DataAccessBuilder
    {
        static ReaderWriterLockSlim _locker = new ReaderWriterLockSlim();

        public static IUnitOfWork CreateUnitOfWork(string info = "")
        {
            /*var options = new DbContextOptionsBuilder<ApplicationContext>().UseSqlServer(info).Options;

            ApplicationContext context = new ApplicationContext(options);

            return new DataUnitOfWork(context, _locker);*/

            return new MockUnitOfWork();
        }
    }
}
