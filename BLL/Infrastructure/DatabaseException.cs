using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Infrastructure
{
    public class DatabaseException : Exception
    {
        public DatabaseException() : base("Database error!") { }
        public DatabaseException(string message) : base("Database error : " + message) { }
    }
}
