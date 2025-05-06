using Domain.Aggregates.User.Entities;
using Domain.Aggregates.User.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            // Configuración de la clave primaria
            builder.HasKey(u => u.Id);
            
            // Configuración de las propiedades de ValueObject
            builder.Property(u => u.Id)
                .HasConversion(
                    id => id.Value,
                    value => UserId.Create(value));
            
            builder.Property(u => u.Email)
                .HasConversion(
                    email => email.Value,
                    value => Email.Create(value).Value)
                .HasMaxLength(256)
                .IsRequired();
            
            builder.Property(u => u.UserName)
                .HasConversion(
                    userName => userName.Value,
                    value => UserName.Create(value).Value)
                .HasMaxLength(50)
                .IsRequired();
            
            builder.Property(u => u.Password)
                .HasConversion(
                    password => password.Value,
                    value => Password.Create(value).Value)
                .IsRequired();
            
            // Configuración de la propiedad compleja (PersonName)
            builder.OwnsOne(u => u.Name, nameBuilder =>
            {
                nameBuilder.Property(n => n.FirstName)
                    .HasColumnName("FirstName")
                    .HasMaxLength(100)
                    .IsRequired();

                nameBuilder.Property(n => n.LastName)
                    .HasColumnName("LastName")
                    .HasMaxLength(100);
            });
            
            // Configuración de la propiedad opcional
            builder.Property(u => u.PhoneNumber)
                .HasConversion(
                    phone => phone != null ? phone.Value : null,
                    value => value != null ? PhoneNumber.Create(value).Value : null)
                .HasMaxLength(20);
            
            // Configuración de la propiedad para integración con Keycloak
            builder.Property(u => u.KeycloakId)
                .HasConversion(
                    id => id != null ? id.Value : null,
                    value => value != null ? ExternalId.Create(value) : null)
                .HasMaxLength(100);
            
            // Índices
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.UserName).IsUnique();
            builder.HasIndex(u => u.KeycloakId).IsUnique().HasFilter("[KeycloakId] IS NOT NULL");
            
            // Relaciones
            builder
                .HasMany(u => u.Roles)
                .WithMany()
                .UsingEntity(j => j.ToTable("UserRoles"));
        }
    }
}