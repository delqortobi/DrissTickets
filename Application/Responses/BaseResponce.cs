namespace Application.Responses;

public class BaseResponce
{
    public BaseResponce() => Success = true;

    public BaseResponce(string message) => Message = message;

    public BaseResponce(string message, bool success)
    {
        Message = message;
        Success = success;
    }

    public bool Success { get; set; }
    public string Message { get; set; }
    public List<string> ValidationErrors { get; set; }
}