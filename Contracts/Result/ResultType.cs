namespace Contracts.Result
{
    public enum ResultType : short
    {
        InternalError = 0,
        Ok = 1,
        NotFound = 2,
        Forbidden = 3,
        Conflicted = 4,
        Invalid = 5,
        Unauthorized = 6,
        BadRequest = 7
    }
}
