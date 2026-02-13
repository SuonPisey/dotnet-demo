namespace MyApi.Models;

public class ApiResponse<T>
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = "";
    public T? Data { get; set; }

    public ApiResponse() { }

    public ApiResponse(T data, string message = "", bool success = true)
    {
        Data = data;
        Message = message;
        Success = success;
    }

    public static ApiResponse<T> Fail(string message)
    {
        return new ApiResponse<T>(default, message, false);
    }
}