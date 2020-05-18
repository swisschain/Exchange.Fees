using System;

namespace Fees.Domain.Exceptions
{
    public class IncorrectAccuracyException : EntityException
    {
        public IncorrectAccuracyException(ErrorCode code, string message) : base(code, message)
        {
        }

        public IncorrectAccuracyException(ErrorCode code, string message, Exception innerException) : base(code, message, innerException)
        {
        }
    }
}
