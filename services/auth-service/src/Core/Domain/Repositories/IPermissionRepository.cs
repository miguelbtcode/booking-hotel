using Domain.Aggregates.Permission.Entities;
using Domain.Aggregates.Permission.ValueObjects;
using Domain.Aggregates.Module.ValueObjects;

namespace Domain.Repositories
{
    public interface IPermissionRepository
    {
        Task<Permission> GetByIdAsync(PermissionId id, CancellationToken cancellationToken = default);
        Task<Permission> GetByCodeAsync(PermissionCode code, CancellationToken cancellationToken = default);
        Task<IEnumerable<Permission>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Permission>> GetByModuleIdAsync(ModuleId moduleId, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(PermissionName name, ModuleId moduleId, CancellationToken cancellationToken = default);
        Task AddAsync(Permission permission, CancellationToken cancellationToken = default);
        Task UpdateAsync(Permission permission, CancellationToken cancellationToken = default);
        Task DeleteAsync(Permission permission, CancellationToken cancellationToken = default);
    }
}