using Domain.Common;

namespace Domain.Aggregates.User.ValueObjects
{
    public class UserId : ValueObject
    {
        public Guid Value { get; }
        
        private UserId(Guid value)
        {
            Value = value;
        }
        
        public static UserId Create(Guid id)
        {
            return new UserId(id);
        }
        
        public static UserId CreateUnique()
        {
            return new UserId(Guid.NewGuid());
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}