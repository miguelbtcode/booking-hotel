using System.Collections.Generic;
using System.Text.RegularExpressions;
using Domain.Aggregates.Module.Errors;
using Domain.Common;

namespace Domain.Aggregates.Module.ValueObjects
{
    public class ModulePath : ValueObject
    {
        public string Value { get; }
        
        private ModulePath(string value)
        {
            Value = value;
        }
        
        public static Result<ModulePath> Create(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return Result.Failure<ModulePath>(ModuleErrors.PathEmpty);
            
            path = path.Trim();
            
            // Validar que la ruta comience con / y no tenga caracteres especiales no permitidos
            // Ajusta la regex según tus necesidades específicas
            if (!Regex.IsMatch(path, @"^\/[a-zA-Z0-9\-_\/]*$"))
                return Result.Failure<ModulePath>(ModuleErrors.PathInvalid);
            
            return Result.Success(new ModulePath(path));
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}