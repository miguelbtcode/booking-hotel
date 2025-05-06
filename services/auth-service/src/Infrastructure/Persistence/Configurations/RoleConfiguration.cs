using Domain.Aggregates.Role.Entities;
using Domain.Aggregates.Role.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            // Configuración de la clave primaria
            builder.HasKey(r => r.Id);
            
            // Configuración de las propiedades de ValueObject
            builder.Property(r => r.Id)
                .HasConversion(
                    id => id.Value,
                    value => RoleId.Create(value));
            
            builder.Property(r => r.Name)
                .HasConversion(
                    name => name.Value,
                    value => RoleName.Create(value).Value)
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(r => r.Code)
                .HasConversion(
                    code => code.Value,
                    value => RoleCode.Create(value).Value)
                .HasMaxLength(50)
                .IsRequired();
            
            builder.Property(r => r.Description)
                .HasMaxLength(500);
            
            builder.Property(r => r.Priority)
                .HasConversion(
                    priority => priority.Value,
                    value => RolePriority.Create(value))
                .IsRequired();
            
            builder.Property(r => r.IsActive)
                .HasDefaultValue(true)
                .IsRequired();
            
            // Configuración de auditoría
            builder.Property(r => r.CreatedAt)
                .IsRequired();
            
            builder.Property(r => r.LastModifiedAt);
            
            // Índices
            builder.HasIndex(r => r.Name).IsUnique();
            builder.HasIndex(r => r.Code).IsUnique();
            
            // Relaciones
            builder
                .HasMany(r => r.Permissions)
                .WithMany()
                .UsingEntity(j => j.ToTable("RolePermissions"));
        }
    }
}