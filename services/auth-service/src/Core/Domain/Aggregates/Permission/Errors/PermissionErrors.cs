using Domain.Common;

namespace Domain.Aggregates.Permission.Errors
{
    public static class PermissionErrors
    {
        public static Error NameEmpty => new(
            "Permission.Name.Empty", 
            "El nombre del permiso no puede estar vacío.");
        
        public static Error NameTooLong => new(
            "Permission.Name.TooLong", 
            "El nombre del permiso no puede exceder los 100 caracteres.");
        
        public static Error DescriptionTooLong => new(
            "Permission.Description.TooLong", 
            "La descripción del permiso no puede exceder los 500 caracteres.");
        
        public static Error DescriptionEmpty => new(
            "Permission.Description.Empty", 
            "La descripción del permiso no puede ser vacía.");

        public static Error ModuleRequired => new(
            "Permission.Module.Required",
            "El permiso debe estar asociado a un módulo.");
        
        public static Error DuplicatePermission => new(
            "Permission.Duplicate", 
            "Ya existe un permiso con este nombre en el módulo especificado.");
        
        public static Error CodeEmpty => new(
            "Permission.Code.Empty", 
            "El código del permiso no puede estar vacío.");
        
        public static Error CodeInvalidFormat => new(
            "Permission.Code.InvalidFormat", 
            "El código del permiso solo puede contener letras mayúsculas, números, guiones y guiones bajos.");
        
        public static Error CodeTooLong => new(
            "Permission.Code.TooLong", 
            "El código del permiso no puede exceder los 50 caracteres.");
    }
}