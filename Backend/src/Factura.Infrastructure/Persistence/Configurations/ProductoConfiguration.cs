using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.src.Factura.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factura.Infrastructure.Persitence.Configurations
{
    public class ProductoConfiguration : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            // La Id viene de BaseEntity
            builder.HasKey(e => e.Id);

            // Hacer el Código obligatorio y único
            builder.Property(e => e.Codigo)
                .IsRequired();
            
            builder.HasIndex(e => e.Codigo)
                .IsUnique();

            // Limitar la descripción para mayor orden
            builder.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsRequired(false); // Permite nulos si así lo definiste en la entidad

            builder.ToTable("Productos");
        }
    }
}