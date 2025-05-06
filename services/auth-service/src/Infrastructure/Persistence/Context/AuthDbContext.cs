using System.Reflection;
using Domain.Aggregates.Permission.Entities;
using Domain.Aggregates.Role.Entities;
using Domain.Aggregates.User.Entities;
using Domain.Common;
using Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context
{
    public class AuthDbContext : DbContext
    {
        private readonly AuditableEntityInterceptor _auditableEntityInterceptor;

        public AuthDbContext(
            DbContextOptions<AuthDbContext> options,
            AuditableEntityInterceptor auditableEntityInterceptor)
            : base(options)
        {
            _auditableEntityInterceptor = auditableEntityInterceptor;
        }

        // Usamos un alias para evitar conflictos con System.Reflection.Module
        public DbSet<Domain.Aggregates.Module.Entities.Module> Modules => Set<Domain.Aggregates.Module.Entities.Module>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplicar todas las configuraciones de entidades
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Aplicar interceptores para auditoría
            optionsBuilder.AddInterceptors(_auditableEntityInterceptor);
            base.OnConfiguring(optionsBuilder);
        }

        // Método para capturar los eventos de dominio y enviarlos 
        public override int SaveChanges()
        {
            CaptureAndDispatchDomainEvents();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            CaptureAndDispatchDomainEvents();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void CaptureAndDispatchDomainEvents()
        {
            var entities = ChangeTracker
                .Entries<Entity<dynamic>>()
                .Where(e => e.Entity.GetDomainEvents().Any())
                .Select(e => e.Entity)
                .ToList();

            var domainEvents = entities
                .SelectMany(e => e.GetDomainEvents())
                .ToList();

            entities.ForEach(e => e.ClearDomainEvents());

            // Aquí puedes procesar los eventos capturados
            // En una implementación real, usarías un despachador de eventos
            // domainEventDispatcher.DispatchEventsAsync(domainEvents);
        }
    }
}