using Domain.Common;

namespace Domain.Aggregates.User.Errors
{
    public static class UserErrors
    {
        public static Error EmailEmpty => new(
            "User.Email.Empty",
            "El email no puede estar vacío.");

        public static Error EmailTooLong => new(
            "User.Email.TooLong",
            "El email no puede exceder los 250 caracteres.");

        public static Error EmailInvalidFormat => new(
            "User.Email.InvalidFormat",
            "El formato del email no es válido.");

        public static Error PasswordEmpty => new(
            "User.Password.Empty",
            "La contraseña no puede estar vacía.");

        public static Error PasswordTooShort => new(
            "User.Password.TooShort",
            "La contraseña debe tener al menos 8 caracteres.");

        public static Error PasswordTooLong => new(
            "User.Password.TooLong",
            "La contraseña no puede exceder los 100 caracteres.");

        public static Error RoleAlreadyAssigned => new(
            "User.Role.AlreadyAssigned",
            "El rol ya ha sido asignado a este usuario.");
            
        public static Error RoleNotAssigned => new(
            "User.Role.NotAssigned",
            "El rol no está asignado a este usuario.");
            
        public static Error EmailNotChanged => new(
            "User.Email.NotChanged",
            "El nuevo email es igual al actual.");

        public static Error UserNameEmpty => new(
            "User.UserName.Empty",
            "El nombre de usuario no puede estar vacío.");

        public static Error UserNameTooShort => new(
            "User.UserName.TooShort",
            "El nombre de usuario debe tener al menos 3 caracteres.");

        public static Error UserNameTooLong => new(
            "User.UserName.TooLong",
            "El nombre de usuario no puede exceder los 50 caracteres.");

        public static Error UserNameInvalidFormat => new(
            "User.UserName.InvalidFormat",
            "El nombre de usuario solo puede contener letras, números, guiones y guiones bajos.");

        public static Error UserNameNotChanged => new(
            "User.UserName.NotChanged",
            "El nuevo nombre de usuario es igual al actual.");

        public static Error PhoneNumberTooShort => new(
            "User.PhoneNumber.TooShort",
            "El número de teléfono debe tener al menos 7 dígitos.");

        public static Error PhoneNumberTooLong => new(
            "User.PhoneNumber.TooLong",
            "El número de teléfono no puede exceder los 15 dígitos.");

        public static Error PhoneNumberInvalidFormat => new(
            "User.PhoneNumber.InvalidFormat",
            "El formato del número de teléfono no es válido.");

        public static Error DuplicateEmail => new(
            "User.Email.Duplicate",
            "Ya existe un usuario con este email.");

        public static Error DuplicateUserName => new(
            "User.UserName.Duplicate",
            "Ya existe un usuario con este nombre de usuario.");

        public static Error UserNotFound => new(
            "User.NotFound",
            "Usuario no encontrado.");

        public static Error InvalidCredentials => new(
            "User.InvalidCredentials",
            "Credenciales inválidas.");
            
        // Agregar estos errores a la clase UserErrors
        public static Error FirstNameEmpty => new(
            "User.FirstName.Empty", 
            "El nombre no puede estar vacío.");
            
        public static Error FirstNameTooLong => new(
            "User.FirstName.TooLong", 
            "El nombre no puede exceder los 100 caracteres.");
            
        public static Error LastNameTooLong => new(
            "User.LastName.TooLong", 
            "El apellido no puede exceder los 100 caracteres.");
    }
}