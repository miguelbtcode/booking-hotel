using System.Collections.Generic;
using Domain.Aggregates.User.Errors;
using Domain.Common;

namespace Domain.Aggregates.User.ValueObjects
{
    public class Password : ValueObject
    {
        public string Value { get; }
        
        // Nunca almacenamos contraseñas en texto plano
        // Esta propiedad estaría hasheada en la implementación real
        
        private Password(string value)
        {
            Value = value;
        }
        
        public static Result<Password> Create(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return Result.Failure<Password>(UserErrors.PasswordEmpty);
            
            if (password.Length < 8)
                return Result.Failure<Password>(UserErrors.PasswordTooShort);
            
            if (password.Length > 100)
                return Result.Failure<Password>(UserErrors.PasswordTooLong);
            
            // Más validaciones como caracteres especiales, números, etc.
            
            return Result.Success(new Password(password));
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}