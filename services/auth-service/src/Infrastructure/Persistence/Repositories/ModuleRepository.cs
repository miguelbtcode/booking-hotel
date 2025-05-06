using Domain.Aggregates.Module.Entities;
using Domain.Aggregates.Module.ValueObjects;
using Domain.Aggregates.User.ValueObjects;
using Domain.Repositories;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly AuthDbContext _context;

        public ModuleRepository(AuthDbContext context)
        {
            _context = context;
        }

        public async Task<Module> GetByIdAsync(ModuleId id, CancellationToken cancellationToken = default)
        {
            return await _context.Modules
                .Include(m => m.Permissions)
                .Include(m => m.SubModules)
                .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Module>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Modules
                .Include(m => m.Permissions)
                .Include(m => m.SubModules)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Module>> GetRootModulesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Modules
                .Include(m => m.Permissions)
                .Include(m => m.SubModules)
                .Where(m => m.ParentModuleId == null)
                .OrderBy(m => m.Order.Value)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Module>> GetSubModulesAsync(ModuleId parentId, CancellationToken cancellationToken = default)
        {
            return await _context.Modules
                .Include(m => m.Permissions)
                .Include(m => m.SubModules)
                .Where(m => m.ParentModuleId == parentId)
                .OrderBy(m => m.Order.Value)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Module>> GetModulesByUserIdAsync(UserId userId, CancellationToken cancellationToken = default)
        {
            // Esta consulta es más compleja ya que necesitamos obtener los módulos
            // basados en los permisos que tiene el usuario a través de sus roles
            var userRoles = await _context.Users
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Roles)
                .ToListAsync(cancellationToken);

            var roleIds = userRoles.Select(r => r.Id).ToList();

            // Obtener todos los permisos de los roles del usuario
            var permissionIds = await _context.Roles
                .Where(r => roleIds.Contains(r.Id))
                .SelectMany(r => r.Permissions)
                .Select(p => p.Id)
                .ToListAsync(cancellationToken);

            // Obtener los módulos que tienen estos permisos
            var modules = await _context.Modules
                .Include(m => m.Permissions)
                .Include(m => m.SubModules)
                .Where(m => m.Permissions.Any(p => permissionIds.Contains(p.Id)))
                .ToListAsync(cancellationToken);

            // También necesitamos incluir los módulos padres para la navegación
            var parentModuleIds = modules
                .Where(m => m.ParentModuleId != null)
                .Select(m => m.ParentModuleId)
                .Distinct()
                .ToList();

            var parentModules = await _context.Modules
                .Include(m => m.Permissions)
                .Include(m => m.SubModules)
                .Where(m => parentModuleIds.Contains(m.Id))
                .ToListAsync(cancellationToken);

            return modules.Concat(parentModules).Distinct().ToList();
        }

        public async Task AddAsync(Module module, CancellationToken cancellationToken = default)
        {
            await _context.Modules.AddAsync(module, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Module module, CancellationToken cancellationToken = default)
        {
            _context.Modules.Update(module);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Module module, CancellationToken cancellationToken = default)
        {
            _context.Modules.Remove(module);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}