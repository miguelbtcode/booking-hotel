using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Aggregates.Role.ValueObjects
{
    public class RoleId : ValueObject
    {
        public Guid Value { get; }
        
        private RoleId(Guid value)
        {
            Value = value;
        }
        
        public static RoleId Create(Guid id)
        {
            return new RoleId(id);
        }
        
        public static RoleId CreateUnique()
        {
            return new RoleId(Guid.NewGuid());
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}