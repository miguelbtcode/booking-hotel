using Domain.Aggregates.Module.ValueObjects;
using Domain.Aggregates.Permission.Errors;
using Domain.Aggregates.Permission.ValueObjects;
using Domain.Common;

namespace Domain.Aggregates.Permission.Entities
{
    public class Permission : Entity<PermissionId>, IAggregateRoot
    {
        public PermissionName Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public PermissionCode Code { get; private set; } = null!;
        public PermissionTypeValue Type { get; private set; } = null!;
        public ModuleId ModuleId { get; private set; } = null!;
        public bool IsActive { get; private set; }
        
        private Permission() { }
        
        private Permission(
            PermissionId id,
            PermissionName name,
            string description,
            PermissionCode code,
            PermissionTypeValue type,
            ModuleId moduleId,
            bool isActive = true) : base(id)
        {
            Name = name;
            Description = description;
            Code = code;
            Type = type;
            ModuleId = moduleId;
            IsActive = isActive;
        }
        
        public static Result<Permission> Create(
            PermissionName name,
            string description,
            PermissionCode code,
            PermissionTypeValue type,
            ModuleId? moduleId)
        {
            if (moduleId is null)
                return Result.Failure<Permission>(PermissionErrors.ModuleRequired);

            if (description == null || description.Length == 0)
                return Result.Failure<Permission>(PermissionErrors.DescriptionEmpty);
                
            if (description != null && description.Length > 500)
                return Result.Failure<Permission>(PermissionErrors.DescriptionTooLong);
                
            return Result.Success(new Permission(
                PermissionId.CreateUnique(),
                name,
                description!,
                code,
                type,
                moduleId));
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
        
        public Result UpdateDetails(
            PermissionName name,
            string description,
            PermissionTypeValue type)
        {
            if (description == null || description.Length == 0)
                return Result.Failure<Permission>(PermissionErrors.DescriptionEmpty);

            if (description != null && description.Length > 500)
                return Result.Failure(PermissionErrors.DescriptionTooLong);
                
            Name = name;
            Description = description!;
            Type = type;
            SetModifiedDate();
            
            return Result.Success();
        }
    }
}