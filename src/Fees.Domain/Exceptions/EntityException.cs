using System;
using System.Collections.Generic;

namespace Fees.Domain.Exceptions
{
    public class EntityException : Exception
    {
        public ErrorCode ErrorCode { get; set; }
        public Dictionary<string, string> Fields { get; set; } = new Dictionary<string, string>();

        public EntityException(ErrorCode code, string message) : base(message)
        {
            ErrorCode = code;
        }

        public EntityException(ErrorCode code, string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = code;
        }
    }

    public static class EntityExceptionExtensions
    {
        public static EntityException AddField(this EntityException exception, string fieldName, string message)
        {
            exception.Fields.Add(fieldName, message);
            return exception;
        }
    }
}
