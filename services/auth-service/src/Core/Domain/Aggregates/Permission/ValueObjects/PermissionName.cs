using System.Collections.Generic;
using Domain.Aggregates.Permission.Errors;
using Domain.Common;

namespace Domain.Aggregates.Permission.ValueObjects
{
    public class PermissionName : ValueObject
    {
        public string Value { get; }
        
        private PermissionName(string value)
        {
            Value = value;
        }
        
        public static Result<PermissionName> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<PermissionName>(PermissionErrors.NameEmpty);
            
            name = name.Trim();
            
            if (name.Length > 100)
                return Result.Failure<PermissionName>(PermissionErrors.NameTooLong);
            
            return Result.Success(new PermissionName(name));
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}