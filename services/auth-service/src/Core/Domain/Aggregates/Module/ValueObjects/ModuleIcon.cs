using System.Collections.Generic;
using Domain.Common;

namespace Domain.Aggregates.Module.ValueObjects
{
    public class ModuleIcon : ValueObject
    {
        public string Value { get; }
        
        private ModuleIcon(string value)
        {
            Value = value;
        }
        
        public static ModuleIcon Create(string icon)
        {
            // Aquí podrías poner validaciones específicas para los iconos
            // Por ejemplo, que sean nombres de iconos válidos de tu sistema (FontAwesome, etc.)
            return new ModuleIcon(icon ?? string.Empty);
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}