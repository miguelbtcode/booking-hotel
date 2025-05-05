using System.Collections.Generic;
using System.Text.RegularExpressions;
using Domain.Aggregates.User.Errors;
using Domain.Common;

namespace Domain.Aggregates.User.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public string Value { get; }
        
        private PhoneNumber(string value)
        {
            Value = value;
        }
        
        public static Result<PhoneNumber> Create(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                return Result.Success<PhoneNumber>(null);
            
            phoneNumber = phoneNumber.Trim();
            
            // Eliminar caracteres no numéricos excepto + al principio
            phoneNumber = Regex.Replace(phoneNumber, @"[^\d+]", "");
            
            if (phoneNumber.Length < 7)
                return Result.Failure<PhoneNumber>(UserErrors.PhoneNumberTooShort);
            
            if (phoneNumber.Length > 15)
                return Result.Failure<PhoneNumber>(UserErrors.PhoneNumberTooLong);
            
            return Result.Success(new PhoneNumber(phoneNumber));
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}