using Domain.Common;

namespace Domain.Aggregates.Role.Errors
{
    public static class RoleErrors
    {
        public static Error NameEmpty => new(
            "Role.Name.Empty", 
            "El nombre del rol no puede estar vacío.");
        
        public static Error NameTooLong => new(
            "Role.Name.TooLong", 
            "El nombre del rol no puede exceder los 100 caracteres.");
        
        public static Error PermissionAlreadyAssigned => new(
            "Role.Permission.AlreadyAssigned", 
            "El permiso ya ha sido asignado a este rol.");
        
        public static Error PermissionNotAssigned => new(
            "Role.Permission.NotAssigned", 
            "El permiso no está asignado a este rol.");
        
        public static Error NameNotChanged => new(
            "Role.Name.NotChanged", 
            "El nuevo nombre es igual al actual.");
        
        public static Error CodeEmpty => new(
            "Role.Code.Empty", 
            "El código del rol no puede estar vacío.");
        
        public static Error CodeInvalidFormat => new(
            "Role.Code.InvalidFormat", 
            "El código del rol solo puede contener letras mayúsculas, números, guiones y guiones bajos.");
        
        public static Error CodeTooLong => new(
            "Role.Code.TooLong", 
            "El código del rol no puede exceder los 50 caracteres.");
            
        public static Error DescriptionEmpty => new(
            "Role.Description.Empty", 
            "La descripción del rol no puede ser vacía.");
        
        public static Error DescriptionTooLong => new(
            "Role.Description.TooLong",
            "La descripción del rol no puede exceder los 500 caracteres.");
        
        public static Error DuplicateRole => new(
            "Role.Duplicate", 
            "Ya existe un rol con este nombre o código.");
            
        public static Error CodeNotChanged => new(
            "Role.Code.NotChanged", 
            "El nuevo código es igual al actual.");
    }
}