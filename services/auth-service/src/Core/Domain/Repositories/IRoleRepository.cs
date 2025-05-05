using Domain.Aggregates.Role.Entities;
using Domain.Aggregates.Role.ValueObjects;

namespace Domain.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> GetByIdAsync(RoleId id, CancellationToken cancellationToken = default);
        Task<Role> GetByNameAsync(RoleName name, CancellationToken cancellationToken = default);
        Task<Role> GetByCodeAsync(RoleCode code, CancellationToken cancellationToken = default);
        Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(RoleName name, RoleCode code, CancellationToken cancellationToken = default);
        Task AddAsync(Role role, CancellationToken cancellationToken = default);
        Task UpdateAsync(Role role, CancellationToken cancellationToken = default);
        Task DeleteAsync(Role role, CancellationToken cancellationToken = default);
    }
}