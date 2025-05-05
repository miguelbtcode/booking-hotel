using Domain.Aggregates.Module.Entities;
using Domain.Aggregates.Module.ValueObjects;
using Domain.Aggregates.User.ValueObjects;

namespace Domain.Repositories
{
    public interface IModuleRepository
    {
        Task<Module> GetByIdAsync(ModuleId id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Module>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Module>> GetRootModulesAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Module>> GetSubModulesAsync(ModuleId parentId, CancellationToken cancellationToken = default);
        Task<IEnumerable<Module>> GetModulesByUserIdAsync(UserId userId, CancellationToken cancellationToken = default);
        Task AddAsync(Module module, CancellationToken cancellationToken = default);
        Task UpdateAsync(Module module, CancellationToken cancellationToken = default);
        Task DeleteAsync(Module module, CancellationToken cancellationToken = default);
    }
}