using Domain.Aggregates.Role.Entities;
using Domain.Aggregates.Role.ValueObjects;
using Domain.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AuthDbContext _context;

        public RoleRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<Role> GetByIdAsync(RoleId id, CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .Include(r => r.Permissions)
                .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }

        public async Task<Role> GetByNameAsync(RoleName name, CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .Include(r => r.Permissions)
                .FirstOrDefaultAsync(r => r.Name.Value == name.Value, cancellationToken);
        }

        public async Task<Role> GetByCodeAsync(RoleCode code, CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .Include(r => r.Permissions)
                .FirstOrDefaultAsync(r => r.Code.Value == code.Value, cancellationToken);
        }

        public async Task<IEnumerable<Role>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .Include(r => r.Permissions)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(RoleName name, RoleCode code, CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .AnyAsync(r => 
                    r.Name.Value == name.Value || 
                    r.Code.Value == code.Value, 
                    cancellationToken);
        }

        public async Task AddAsync(Role role, CancellationToken cancellationToken = default)
        {
            await _context.Roles.AddAsync(role, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Role role, CancellationToken cancellationToken = default)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Role role, CancellationToken cancellationToken = default)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}