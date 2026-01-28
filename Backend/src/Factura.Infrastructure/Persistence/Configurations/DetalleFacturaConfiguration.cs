using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.src.Factura.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factura.Infrastructure.Persitence.Configurations
{
    public class DetalleFacturaConfiguration : IEntityTypeConfiguration<DetalleFactura>
    {
        public void Configure(EntityTypeBuilder<DetalleFactura> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Cantidad)
                .IsRequired();

            // Configuración de precisión para dinero (numeric 18,2)
            builder.Property(e => e.PrecioUnitario)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(e => e.Subtotal)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(e => e.Total)
                .HasPrecision(18, 2)
                .IsRequired();

            // Relación con el Producto
            builder.HasOne(e => e.Producto)
                .WithMany() // Un producto puede estar en muchos detalles
                .HasForeignKey(e => e.ProductoId)
                .OnDelete(DeleteBehavior.Restrict); // No borrar productos si tienen ventas

            // Relación con la Factura (Padre)
            builder.HasOne(e => e.Invoice)
                .WithMany(i => i.DetalleFacturas) // Acceso desde Invoice a sus detalles
                .HasForeignKey(e => e.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade); // Si se borra la factura, se borra el detalle

            builder.ToTable("DetalleFacturas");
        }
    }
}