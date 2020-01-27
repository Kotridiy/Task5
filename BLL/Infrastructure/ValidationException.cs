using System;

namespace BLL.Infrastructure
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("Validation error!") { }
        public ValidationException(string message) : base("Valudation error : " + message) { }
    }
}
