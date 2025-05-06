using Domain.Aggregates.Permission.Entities;
using Domain.Aggregates.Permission.ValueObjects;
using Domain.Aggregates.Module.ValueObjects;
using Domain.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly AuthDbContext _context;

        public PermissionRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<Permission> GetByIdAsync(PermissionId id, CancellationToken cancellationToken = default)
        {
            return await _context.Permissions
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<Permission> GetByCodeAsync(PermissionCode code, CancellationToken cancellationToken = default)
        {
            return await _context.Permissions
                .FirstOrDefaultAsync(p => p.Code.Value == code.Value, cancellationToken);
        }

        public async Task<IEnumerable<Permission>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Permissions
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Permission>> GetByModuleIdAsync(ModuleId moduleId, CancellationToken cancellationToken = default)
        {
            return await _context.Permissions
                .Where(p => p.ModuleId == moduleId)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(PermissionName name, ModuleId moduleId, CancellationToken cancellationToken = default)
        {
            return await _context.Permissions
                .AnyAsync(p => 
                    p.Name.Value == name.Value && 
                    p.ModuleId == moduleId, 
                    cancellationToken);
        }

        public async Task AddAsync(Permission permission, CancellationToken cancellationToken = default)
        {
            await _context.Permissions.AddAsync(permission, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Permission permission, CancellationToken cancellationToken = default)
        {
            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Permission permission, CancellationToken cancellationToken = default)
        {
            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}