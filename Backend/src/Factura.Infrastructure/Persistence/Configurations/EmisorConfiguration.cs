using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.src.Factura.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factura.Infrastructure.Persitence.Configurations
{
    public class EmisorConfiguration : IEntityTypeConfiguration<Emisor>
    {
        public void Configure(EntityTypeBuilder<Emisor> builder)
        {
            // Id heredada de BaseEntity
            builder.HasKey(e => e.Id);

            // NIT único y obligatorio
            builder.Property(e => e.Nit)
                .IsRequired()
                .HasMaxLength(20);
            
            builder.HasIndex(e => e.Nit)
                .IsUnique();

  
            builder.Property(e => e.RazonSocial)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(e => e.Direccion)
                .HasMaxLength(200);

            builder.Property(e => e.Telefono)
                .HasMaxLength(20);

            // Email corporativo único
            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(e => e.Email)
                .IsUnique();

            // Relación inversa: Un emisor tiene muchas facturas
            builder.HasMany(e => e.Invoices)
                .WithOne(i => i.Emisor)
                .HasForeignKey(i => i.EmisorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Emisores");
        }
    }
}