using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Aggregates.Permission.ValueObjects
{
    public class PermissionId : ValueObject
    {
        public Guid Value { get; }
        
        private PermissionId(Guid value)
        {
            Value = value;
        }
        
        public static PermissionId Create(Guid id)
        {
            return new PermissionId(id);
        }
        
        public static PermissionId CreateUnique()
        {
            return new PermissionId(Guid.NewGuid());
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}