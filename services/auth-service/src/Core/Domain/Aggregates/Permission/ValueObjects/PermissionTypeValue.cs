using System.Collections.Generic;
using Domain.Aggregates.Permission.Enums;
using Domain.Common;

namespace Domain.Aggregates.Permission.ValueObjects
{
    public class PermissionTypeValue : ValueObject
    {
        public PermissionType Value { get; }
        
        private PermissionTypeValue(PermissionType value)
        {
            Value = value;
        }
        
        public static PermissionTypeValue Create(PermissionType type)
        {
            return new PermissionTypeValue(type);
        }
        
        public static PermissionTypeValue Read => new(PermissionType.Read);
        public static PermissionTypeValue Write => new(PermissionType.Write);
        public static PermissionTypeValue Delete => new(PermissionType.Delete);
        public static PermissionTypeValue Execute => new(PermissionType.Execute);
        public static PermissionTypeValue Special => new(PermissionType.Special);
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}