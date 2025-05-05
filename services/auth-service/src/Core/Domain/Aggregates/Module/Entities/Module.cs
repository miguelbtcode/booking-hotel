using Domain.Aggregates.Module.Errors;
using Domain.Aggregates.Module.ValueObjects;
using Domain.Common;

namespace Domain.Aggregates.Module.Entities
{
    public class Module : Entity<ModuleId>, IAggregateRoot
    {
        public ModuleName Name { get; private set; } = null!;
        public string Description { get; private set; } = null!;
        public ModulePath Path { get; private set; } = null!;
        public ModuleIcon Icon { get; private set; } = null!;
        public DisplayOrder Order { get; private set; } = null!;
        public bool IsActive { get; private set; }
        
        private readonly List<Permission.Entities.Permission> _permissions = [];
        public IReadOnlyCollection<Permission.Entities.Permission> Permissions => _permissions.AsReadOnly();
        
        private readonly List<Module> _subModules = [];
        public IReadOnlyCollection<Module> SubModules => _subModules.AsReadOnly();
        
        public ModuleId? ParentModuleId { get; private set; }
        
        private Module() { } // Para EF Core
        
        private Module(
            ModuleId id,
            ModuleName name,
            string description,
            ModulePath path,
            ModuleIcon icon,
            DisplayOrder order,
            bool isActive = true,
            ModuleId? parentModuleId = null) : base(id)
        {
            Id = id;
            Name = name;
            Description = description;
            Path = path;
            Icon = icon;
            Order = order;
            IsActive = isActive;
            ParentModuleId = parentModuleId;
        }
        
        public static Result<Module> Create(
            ModuleName name,
            string description,
            ModulePath path,
            ModuleIcon icon,
            DisplayOrder? order = null,
            bool isActive = true,
            ModuleId? parentModuleId = null)
        {
            return Result.Success(new Module(
                ModuleId.CreateUnique(),
                name,
                description,
                path,
                icon,
                order ?? DisplayOrder.Default,
                isActive,
                parentModuleId));
        }
        
        public Result AddSubModule(Module subModule)
        {
            // Validar que no se esté intentando agregar a sí mismo como submódulo
            if (subModule.Id == Id)
                return Result.Failure(ModuleErrors.CircularReference);
                
            // Validación para evitar referencias circulares
            if (IsSubModuleOf(subModule.Id))
                return Result.Failure(ModuleErrors.CircularReference);
                
            if (_subModules.Contains(subModule))
                return Result.Failure(ModuleErrors.SubModuleAlreadyAdded);
            
            _subModules.Add(subModule);
            subModule.SetParentModule(Id);
            SetModifiedDate();
            
            return Result.Success();
        }
        
        private void SetParentModule(ModuleId parentId)
        {
            ParentModuleId = parentId;
            SetModifiedDate();
        }
        
        // Método para verificar si este módulo es submódulo (directo o indirecto) de otro
        private bool IsSubModuleOf(ModuleId moduleId)
        {
            if (ParentModuleId is null)
                return false;
                    
            if (ParentModuleId.Value.Equals(moduleId.Value))
                return true;
                    
            return false;
        }
        
        public Result AddPermission(Permission.Entities.Permission permission)
        {
            if (_permissions.Contains(permission))
                return Result.Failure(ModuleErrors.PermissionAlreadyAdded);
            
            _permissions.Add(permission);
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
        
        public Result UpdateDetails(
            ModuleName name,
            string description,
            ModulePath path,
            ModuleIcon icon,
            DisplayOrder order)
        {
            Name = name;
            Description = description;
            Path = path;
            Icon = icon;
            Order = order;
            SetModifiedDate();
            
            return Result.Success();
        }
    }
}