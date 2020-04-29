namespace Fees.Exceptions
{
    internal class ResponseModel<T> : ResponseModel
    {
        public T Payload { get; set; }

        public static ResponseModel<T> Ok(T payload)
        {
            return new ResponseModel<T> { Payload = payload };
        }
    }

    internal class ResponseModel
    {
        public string Error { get; set; }

        public static ResponseModel Fail(string message)
        {
            return new ResponseModel { Error = message };
        }
    }
}
