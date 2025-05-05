namespace Domain.Common;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }
    
    protected Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
            throw new InvalidOperationException("No se puede marcar como exitoso un resultado con errores");
        
        if (!isSuccess && error == Error.None)
            throw new InvalidOperationException("No se puede marcar como fallido un resultado sin errores");
        
        IsSuccess = isSuccess;
        Error = error;
    }
    
    public static Result Success() => new(true, Error.None);
    
    public static Result Failure(Error error) => new(false, error);
    
    public static Result<T> Success<T>(T? value) => new(value, true, Error.None);
    
    public static Result<T> Failure<T>(Error error) => new(default!, false, error);
}

public class Result<T> : Result
{
    private readonly T? _value;
    
    public T Value
    {
        get
        {
            if (IsFailure)
                throw new InvalidOperationException("No se puede acceder al valor de un resultado fallido.");
            
            return _value ?? default!;
        }
    }
    
    protected internal Result(T? value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }
}