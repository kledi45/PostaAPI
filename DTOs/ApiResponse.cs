namespace PostaAPI.DTOs
{
    public class ApiResponse<T>
    {
        public ApiResponse(int statusCode)
        {
            StatusCode = statusCode;
        }
        public ApiResponse(int statusCode, T? result)
        {
            StatusCode = statusCode;
            Result = result;
        }
        public int StatusCode { get; set; }
        public T? Result { get; set; }
    }
}
