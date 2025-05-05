using System.Collections.Generic;
using Domain.Common;

namespace Domain.Aggregates.Module.ValueObjects
{
    public class DisplayOrder : ValueObject
    {
        public int Value { get; }
        
        private DisplayOrder(int value)
        {
            Value = value;
        }
        
        public static DisplayOrder Create(int order)
        {
            // El orden debe ser un número positivo
            return new DisplayOrder(order < 0 ? 0 : order);
        }
        
        public static DisplayOrder Default => new(0);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}