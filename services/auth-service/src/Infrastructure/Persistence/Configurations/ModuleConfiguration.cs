using Domain.Aggregates.Module.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Module = Domain.Aggregates.Module.Entities.Module;

namespace Infrastructure.Persistence.Configurations
{
    public class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable("Modules");

            // Configuración de la clave primaria
            builder.HasKey(m => m.Id);
            
            // Configuración de las propiedades de ValueObject
            builder.Property(m => m.Id)
                .HasConversion(
                    id => id.Value,
                    value => ModuleId.Create(value));
            
            builder.Property(m => m.Name)
                .HasConversion(
                    name => name.Value,
                    value => ModuleName.Create(value).Value)
                .HasMaxLength(100)
                .IsRequired();
            
            builder.Property(m => m.Description)
                .HasMaxLength(500);
            
            builder.Property(m => m.Path)
                .HasConversion(
                    path => path.Value,
                    value => ModulePath.Create(value).Value)
                .HasMaxLength(200)
                .IsRequired();
            
            builder.Property(m => m.Icon)
                .HasConversion(
                    icon => icon.Value,
                    value => ModuleIcon.Create(value))
                .HasMaxLength(100);
            
            builder.Property(m => m.Order)
                .HasConversion(
                    order => order.Value,
                    value => DisplayOrder.Create(value))
                .IsRequired();
            
            builder.Property(m => m.IsActive)
                .HasDefaultValue(true)
                .IsRequired();
            
            builder.Property(m => m.ParentModuleId)
                .HasConversion(
                    id => id != null ? id.Value : (Guid?)null,
                    value => value != null ? ModuleId.Create(value.Value) : null);
            
            // Configuración de auditoría
            builder.Property(m => m.CreatedAt)
                .IsRequired();
            
            builder.Property(m => m.LastModifiedAt);
            
            // Índices
            builder.HasIndex(m => m.Name);
            builder.HasIndex(m => m.Path).IsUnique();
            
            // Relaciones
            builder
                .HasMany(m => m.SubModules)
                .WithOne()
                .HasForeignKey("ParentModuleId")
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}