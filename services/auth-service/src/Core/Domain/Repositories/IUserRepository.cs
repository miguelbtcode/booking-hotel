using Domain.Aggregates.Role.ValueObjects;
using Domain.Aggregates.User.Entities;
using Domain.Aggregates.User.ValueObjects;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(UserId id, CancellationToken cancellationToken = default);
        Task<User> GetByEmailAsync(Email email, CancellationToken cancellationToken = default);
        Task<User> GetByUserNameAsync(UserName userName, CancellationToken cancellationToken = default);
        Task<User> GetByExternalIdAsync(string externalId, CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> GetByRoleAsync(RoleId roleId, CancellationToken cancellationToken = default);
        Task<bool> ExistsEmailAsync(Email email, CancellationToken cancellationToken = default);
        Task<bool> ExistsUserNameAsync(UserName userName, CancellationToken cancellationToken = default);
        Task AddAsync(User user, CancellationToken cancellationToken = default);
        Task UpdateAsync(User user, CancellationToken cancellationToken = default);
        Task DeleteAsync(User user, CancellationToken cancellationToken = default);
    }
}