namespace Fees.Domain.Exceptions
{
    public enum ErrorCode
    {
        //1000 - general server network
        //1001 - 500 RuntimeError
        //1100 - validation
        //2000 - logic errors, i.e. from ME
        RuntimeError = 1001,
        ItemNotFound = 1100,
        DuplicateItem = 1101
    }
}
