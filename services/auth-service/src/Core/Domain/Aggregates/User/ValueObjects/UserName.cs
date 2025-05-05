using System.Collections.Generic;
using System.Text.RegularExpressions;
using Domain.Aggregates.User.Errors;
using Domain.Common;

namespace Domain.Aggregates.User.ValueObjects
{
    public class UserName : ValueObject
    {
        public string Value { get; }
        
        private UserName(string value)
        {
            Value = value;
        }
        
        public static Result<UserName> Create(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return Result.Failure<UserName>(UserErrors.UserNameEmpty);
            
            userName = userName.Trim();
            
            if (userName.Length < 3)
                return Result.Failure<UserName>(UserErrors.UserNameTooShort);
            
            if (userName.Length > 50)
                return Result.Failure<UserName>(UserErrors.UserNameTooLong);
            
            // Solo permitir letras, números, guiones y guiones bajos
            if (!Regex.IsMatch(userName, @"^[a-zA-Z0-9\-_]+$"))
                return Result.Failure<UserName>(UserErrors.UserNameInvalidFormat);
            
            return Result.Success(new UserName(userName));
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}