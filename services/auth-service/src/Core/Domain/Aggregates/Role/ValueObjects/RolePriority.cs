using System.Collections.Generic;
using Domain.Common;

namespace Domain.Aggregates.Role.ValueObjects
{
    public class RolePriority : ValueObject
    {
        public int Value { get; }
        
        private RolePriority(int value)
        {
            Value = value;
        }
        
        public static RolePriority Create(int priority)
        {
            // Asegurar que la prioridad sea un número positivo
            return new RolePriority(priority < 0 ? 0 : priority);
        }
        
        public static RolePriority Default => new(0);
        
        public static RolePriority Lowest => new(0);
        
        public static RolePriority Highest => new(999);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}