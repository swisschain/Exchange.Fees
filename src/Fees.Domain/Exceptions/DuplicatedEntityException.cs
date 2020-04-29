using System;

namespace Fees.Domain.Exceptions
{
    public class DuplicatedEntityException : EntityException
    {
        public DuplicatedEntityException(ErrorCode code, string message) : base(code, message)
        {
        }

        public DuplicatedEntityException(ErrorCode code, string message, Exception innerException) : base(code, message, innerException)
        {
        }
    }
}
