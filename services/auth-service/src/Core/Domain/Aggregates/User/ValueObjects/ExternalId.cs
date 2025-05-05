using Domain.Common;

namespace Domain.Aggregates.User.ValueObjects
{
    public class ExternalId : ValueObject
    {
        public string Value { get; }
        
        private ExternalId(string value)
        {
            Value = value;
        }
        
        public static ExternalId Create(string externalId)
        {
            return new ExternalId(externalId);
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}