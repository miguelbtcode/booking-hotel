using System.Collections.Generic;
using Domain.Aggregates.Role.Errors;
using Domain.Common;

namespace Domain.Aggregates.Role.ValueObjects
{
    public class RoleName : ValueObject
    {
        public string Value { get; }
        
        private RoleName(string value)
        {
            Value = value;
        }
        
        public static Result<RoleName> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Failure<RoleName>(RoleErrors.NameEmpty);
            
            name = name.Trim();
            
            if (name.Length > 100)
                return Result.Failure<RoleName>(RoleErrors.NameTooLong);
            
            return Result.Success(new RoleName(name));
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}