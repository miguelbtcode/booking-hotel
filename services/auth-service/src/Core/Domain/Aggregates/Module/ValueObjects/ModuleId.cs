using Domain.Common;

namespace Domain.Aggregates.Module.ValueObjects;

public class ModuleId : ValueObject
{
    public Guid Value { get; }
    
    private ModuleId(Guid value)
    {
        Value = value;
    }
    
    public static ModuleId Create(Guid id)
    {
        return new ModuleId(id);
    }
    
    public static ModuleId CreateUnique()
    {
        return new ModuleId(Guid.NewGuid());
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}