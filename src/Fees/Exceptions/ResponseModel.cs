﻿using System.Collections.Generic;

namespace Fees.Exceptions
{
    public class ResponseModel<T> : ResponseModel
    {
        public T Payload { get; set; }

        public static ResponseModel<T> Ok(T payload)
        {
            return new ResponseModel<T> { Payload = payload };
        }
    }

    public class ResponseModel
    {
        public ErrorModel Error { get; set; }

        public static ResponseModel Fail(int code, string message, Dictionary<string, string> fields)
        {
            return new ResponseModel { Error = new ErrorModel { Code = code, Message = message, Fields = fields } };
        }
    }

    public class ErrorModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public Dictionary<string, string> Fields { get; set; } = new Dictionary<string, string>();
    }
}
