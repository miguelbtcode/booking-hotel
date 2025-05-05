using System.Collections.Generic;
using System.Text.RegularExpressions;
using Domain.Aggregates.Role.Errors;
using Domain.Common;

namespace Domain.Aggregates.Role.ValueObjects
{
    public class RoleCode : ValueObject
    {
        public string Value { get; }
        
        private RoleCode(string value)
        {
            Value = value;
        }
        
        public static Result<RoleCode> Create(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return Result.Failure<RoleCode>(RoleErrors.CodeEmpty);
            
            code = code.Trim().ToUpperInvariant();
            
            // Validar formato: solo letras, números, guiones y guiones bajos
            if (!Regex.IsMatch(code, @"^[A-Z0-9\-_]+$"))
                return Result.Failure<RoleCode>(RoleErrors.CodeInvalidFormat);
            
            if (code.Length > 50)
                return Result.Failure<RoleCode>(RoleErrors.CodeTooLong);
            
            return Result.Success(new RoleCode(code));
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}