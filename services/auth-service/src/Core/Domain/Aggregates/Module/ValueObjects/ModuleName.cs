using Domain.Aggregates.Module.Errors;
using Domain.Common;

namespace Domain.Aggregates.Module.ValueObjects;

public class ModuleName : ValueObject
{
    public string Value { get; }
    
    private ModuleName(string value)
    {
        Value = value;
    }
    
    public static Result<ModuleName> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<ModuleName>(ModuleErrors.NameEmpty);
        
        name = name.Trim();
        
        if (name.Length > 100)
            return Result.Failure<ModuleName>(ModuleErrors.NameTooLong);
        
        return Result.Success(new ModuleName(name));
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}