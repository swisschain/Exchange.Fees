using System;

namespace Fees.Domain.Exceptions
{
    public class DuplicatedEntityException : EntityException
    {
        public DuplicatedEntityException(string message) : base(message)
        {
        }

        public DuplicatedEntityException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
