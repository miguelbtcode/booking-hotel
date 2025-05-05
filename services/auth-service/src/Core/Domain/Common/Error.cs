namespace Domain.Common;

public class Error
{
    public string Code { get; }
    public string Message { get; }
    
    public Error(string code, string message)
    {
        Code = code;
        Message = message;
    }
    
    public static Error None => new(string.Empty, string.Empty);
    
    public static implicit operator string(Error error) => error.Code;
}