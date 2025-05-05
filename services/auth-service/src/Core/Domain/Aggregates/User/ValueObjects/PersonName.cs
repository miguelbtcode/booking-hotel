using System.Collections.Generic;
using Domain.Aggregates.User.Errors;
using Domain.Common;

namespace Domain.Aggregates.User.ValueObjects
{
    public class PersonName : ValueObject
    {
        public string FirstName { get; }
        public string LastName { get; }
        
        private PersonName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        
        public static Result<PersonName> Create(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return Result.Failure<PersonName>(UserErrors.FirstNameEmpty);
                
            firstName = firstName.Trim();
            
            if (firstName.Length > 100)
                return Result.Failure<PersonName>(UserErrors.FirstNameTooLong);
                
            if (lastName != null)
            {
                lastName = lastName.Trim();
                
                if (lastName.Length > 100)
                    return Result.Failure<PersonName>(UserErrors.LastNameTooLong);
            }
            
            return Result.Success(new PersonName(firstName, lastName ?? string.Empty));
        }
        
        public string FullName => $"{FirstName} {LastName}".Trim();
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}