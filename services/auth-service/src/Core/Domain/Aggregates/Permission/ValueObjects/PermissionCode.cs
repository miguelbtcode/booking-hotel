using System.Collections.Generic;
using System.Text.RegularExpressions;
using Domain.Aggregates.Permission.Errors;
using Domain.Common;

namespace Domain.Aggregates.Permission.ValueObjects
{
    public class PermissionCode : ValueObject
    {
        public string Value { get; }
        
        private PermissionCode(string value)
        {
            Value = value;
        }
        
        public static Result<PermissionCode> Create(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return Result.Failure<PermissionCode>(PermissionErrors.CodeEmpty);
            
            code = code.Trim().ToUpperInvariant();
            
            // Validar formato: solo letras, números y guiones
            if (!Regex.IsMatch(code, @"^[A-Z0-9\-_]+$"))
                return Result.Failure<PermissionCode>(PermissionErrors.CodeInvalidFormat);
            
            if (code.Length > 50)
                return Result.Failure<PermissionCode>(PermissionErrors.CodeTooLong);
            
            return Result.Success(new PermissionCode(code));
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}