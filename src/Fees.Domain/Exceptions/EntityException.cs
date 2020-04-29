using System;

namespace Fees.Domain.Exceptions
{
    public class EntityException : Exception
    {
        public EntityException(string message) : base(message)
        {
        }

        public EntityException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
