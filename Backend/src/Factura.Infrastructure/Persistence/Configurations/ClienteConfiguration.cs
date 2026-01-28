using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Factura.Domain.Entities;
using Backend.src.Factura.Domain.Entities;

namespace Factura.Infrastructure.Persitence.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            // Id heredada de BaseEntity
            builder.HasKey(e => e.Id);
        
            builder.Property(e => e.Nit)
                .HasMaxLength(50);

            builder.Property(e => e.Nombres)
                .IsRequired() // Los nombres son obligatorios
                .HasMaxLength(100);

            builder.Property(e => e.Apellidos)
                .IsRequired()
                .HasMaxLength(100);

            // Hacer el Email único y obligatorio
            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(150);
            
            builder.HasIndex(e => e.Email)
                .IsUnique();

            builder.Property(e => e.Telefono)
                .HasMaxLength(20);

            builder.Property(e => e.Direccion)
                .HasMaxLength(200);

            // Definición explícita de la relación 1:N
            builder.HasMany(e => e.Invoices)
                .WithOne(i => i.Cliente)
                .HasForeignKey(i => i.ClienteId)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.ToTable("Clientes");
        }
    }
}