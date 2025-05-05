using System.Collections.Generic;
using System.Text.RegularExpressions;
using Domain.Aggregates.User.Errors;
using Domain.Common;

namespace Domain.Aggregates.User.ValueObjects
{
    public class Email : ValueObject
    {
        public string Value { get; }
        
        private Email(string value)
        {
            Value = value;
        }
        
        public static Result<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result.Failure<Email>(UserErrors.EmailEmpty);
            
            email = email.Trim();
            
            if (email.Length > 250)
                return Result.Failure<Email>(UserErrors.EmailTooLong);
            
            if (!Regex.IsMatch(email, @"^(.+)@(.+)$"))
                return Result.Failure<Email>(UserErrors.EmailInvalidFormat);
            
            return Result.Success(new Email(email));
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}