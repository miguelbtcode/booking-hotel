using Domain.Aggregates.Role.ValueObjects;
using Domain.Aggregates.User.Entities;
using Domain.Aggregates.User.ValueObjects;
using Domain.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthDbContext _context;

        public UserRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetByIdAsync(UserId id, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        }

        public async Task<User> GetByEmailAsync(Email email, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Email.Value == email.Value, cancellationToken);
        }

        public async Task<User> GetByUserNameAsync(UserName userName, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.UserName.Value == userName.Value, cancellationToken);
        }

        public async Task<User> GetByExternalIdAsync(string externalId, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.KeycloakId != null && u.KeycloakId.Value == externalId, cancellationToken);
        }

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .Include(u => u.Roles)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<User>> GetByRoleAsync(RoleId roleId, CancellationToken cancellationToken = default)
        {
            return await _context.Users
                .Include(u => u.Roles)
                .Where(u => u.Roles.Any(r => r.Id == roleId))
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsEmailAsync(Email email, CancellationToken cancellationToken = default)
        {
            return await _context.Users.AnyAsync(u => u.Email.Value == email.Value, cancellationToken);
        }

        public async Task<bool> ExistsUserNameAsync(UserName userName, CancellationToken cancellationToken = default)
        {
            return await _context.Users.AnyAsync(u => u.UserName.Value == userName.Value, cancellationToken);
        }

        public async Task AddAsync(User user, CancellationToken cancellationToken = default)
        {
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(User user, CancellationToken cancellationToken = default)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}