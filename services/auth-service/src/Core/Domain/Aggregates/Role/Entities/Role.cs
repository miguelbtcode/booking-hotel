using Domain.Aggregates.Role.Errors;
using Domain.Aggregates.Role.ValueObjects;
using Domain.Common;

namespace Domain.Aggregates.Role.Entities
{
    public class Role : Entity<RoleId>, IAggregateRoot
    {
        public RoleName Name { get; private set; } = null!;
        public RoleCode Code { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public RolePriority Priority { get; private set; } = null!;
        public bool IsActive { get; private set; }
        
        private readonly List<Permission.Entities.Permission> _permissions = [];
        public IReadOnlyCollection<Permission.Entities.Permission> Permissions => _permissions.AsReadOnly();
        
        private Role() { } // Para EF Core
        
        private Role(
            RoleId id,
            RoleName name,
            RoleCode code,
            string description,
            RolePriority priority,
            bool isActive = true) : base(id)
        {
            Id = id;
            Name = name;
            Code = code;
            Description = description;
            Priority = priority;
            IsActive = isActive;
        }
        
        public static Result<Role> Create(
            RoleName name,
            RoleCode code,
            string? description,
            RolePriority? priority = null)
        {
            if (description == null || description.Length == 0)
                return Result.Failure<Role>(RoleErrors.DescriptionEmpty);
            if (description != null && description.Length > 500)
                return Result.Failure<Role>(RoleErrors.DescriptionTooLong);
                
            return Result.Success(new Role(
                RoleId.CreateUnique(),
                name,
                code,
                description!,
                priority ?? RolePriority.Default,
                true));
        }
        
        public Result AssignPermission(Permission.Entities.Permission permission)
        {
            if (_permissions.Contains(permission))
                return Result.Failure(RoleErrors.PermissionAlreadyAssigned);
            
            _permissions.Add(permission);
            SetModifiedDate();
            
            return Result.Success();
        }
        
        public Result RemovePermission(Permission.Entities.Permission permission)
        {
            if (!_permissions.Contains(permission))
                return Result.Failure(RoleErrors.PermissionNotAssigned);
            
            _permissions.Remove(permission);
            SetModifiedDate();
            
            return Result.Success();
        }
        
        public Result ChangeName(RoleName name)
        {
            if (Name.Equals(name))
                return Result.Failure(RoleErrors.NameNotChanged);
            
            Name = name;
            SetModifiedDate();
            
            return Result.Success();
        }
        
        public Result ChangeCode(RoleCode code)
        {
            if (Code.Equals(code))
                return Result.Failure(RoleErrors.CodeNotChanged);
            
            Code = code;
            SetModifiedDate();
            
            return Result.Success();
        }
        
        public Result UpdateDetails(
            RoleName name,
            string? description,
            RolePriority priority)
        {
            if (description == null || description.Length == 0)
                return Result.Failure<Role>(RoleErrors.DescriptionEmpty);
            if (description != null && description.Length > 500)
                return Result.Failure(RoleErrors.DescriptionTooLong);
                
            Name = name;
            Description = description!;
            Priority = priority;
            SetModifiedDate();
            
            return Result.Success();
        }
        
        public void Activate()
        {
            if (IsActive)
                return;
                
            IsActive = true;
            SetModifiedDate();
        }
        
        public void Deactivate()
        {
            if (!IsActive)
                return;
                
            IsActive = false;
            SetModifiedDate();
        }
    }
}