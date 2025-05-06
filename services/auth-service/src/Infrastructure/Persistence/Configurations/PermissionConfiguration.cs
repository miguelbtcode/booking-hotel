using Domain.Aggregates.Permission.Entities;
using Domain.Aggregates.Permission.ValueObjects;
using Domain.Aggregates.Module.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");

            // Configuración de la clave primaria
            builder.HasKey(p => p.Id);

            // Configuración de las propiedades de ValueObject
            builder.Property(p => p.Id)
                .HasConversion(
                    id => id.Value,
                    value => PermissionId.Create(value));

            builder.Property(p => p.Name)
                .HasConversion(
                    name => name.Value,
                    value => PermissionName.Create(value).Value)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Code)
                .HasConversion(
                    code => code.Value,
                    value => PermissionCode.Create(value).Value)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.Type)
                .HasConversion(
                    type => (int)type.Value,
                    value => PermissionTypeValue.Create((Domain.Aggregates.Permission.Enums.PermissionType)value))
                .IsRequired();

            builder.Property(p => p.ModuleId)
                .HasConversion(
                    id => id.Value,
                    value => ModuleId.Create(value))
                .IsRequired();

            builder.Property(p => p.IsActive)
                .HasDefaultValue(true)
                .IsRequired();

            // Configuración de auditoría
            builder.Property(p => p.CreatedAt)
                .IsRequired();

            builder.Property(p => p.LastModifiedAt);

            // Índices
            builder.HasIndex(p => p.Code).IsUnique();
            builder.HasIndex(p => new { p.Name, p.ModuleId }).IsUnique();

            // Relaciones
            builder.HasOne<Domain.Aggregates.Module.Entities.Module>()
                .WithMany(m => m.Permissions)
                .HasForeignKey(p => p.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}