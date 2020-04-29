using System;

namespace Fees.Domain.Exceptions
{
    public class EntityNotFoundException : EntityException
    {
        public EntityNotFoundException(ErrorCode code, string message) : base(code, message)
        {
        }

        public EntityNotFoundException(ErrorCode code, string message, Exception innerException) : base(code, message, innerException)
        {
        }
    }
}
