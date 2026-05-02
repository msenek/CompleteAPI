namespace TestAPI.Middleware
{
    public class ApiException : Exception
    {
        public int statusCode { get; }
        public ApiException(string messange, int statusCode) : base(messange)
        {
            statusCode = statusCode;
        }
    }
}
