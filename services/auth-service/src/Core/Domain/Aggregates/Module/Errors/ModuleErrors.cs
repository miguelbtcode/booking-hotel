using Domain.Common;

namespace Domain.Aggregates.Module.Errors;

public static class ModuleErrors
{
    public static Error NameEmpty => new(
        "Module.Name.Empty", 
        "El nombre del módulo no puede estar vacío.");
    
    public static Error NameTooLong => new(
        "Module.Name.TooLong", 
        "El nombre del módulo no puede exceder los 100 caracteres.");
    
    public static Error PathEmpty => new(
        "Module.Path.Empty", 
        "La ruta del módulo no puede estar vacía.");
    
    public static Error PathInvalid => new(
        "Module.Path.Invalid", 
        "La ruta del módulo tiene un formato inválido.");
    
    public static Error SubModuleAlreadyAdded => new(
        "Module.SubModule.AlreadyAdded", 
        "El sub-módulo ya ha sido agregado a este módulo.");
    
    public static Error PermissionAlreadyAdded => new(
        "Module.Permission.AlreadyAdded", 
        "El permiso ya ha sido agregado a este módulo.");
    
    public static Error ParentModuleNotFound => new(
        "Module.ParentModule.NotFound", 
        "El módulo padre especificado no existe.");
    
    public static Error CircularReference => new(
        "Module.CircularReference", 
        "No se puede agregar un módulo padre como submódulo, causaría una referencia circular.");
}