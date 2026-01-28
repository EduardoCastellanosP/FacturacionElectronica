using Backend.src.Factura.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factura.Infrastructure.Persitence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            // Id heredada de BaseEntity
            builder.HasKey(p => p.Id);

            builder.Property(p => p.FechaPago)
                .IsRequired();

            // Configuración para el método de pago
            builder.Property(p => p.MetodoPago)
                .IsRequired()
                .HasMaxLength(50);

            // Relación con la Factura (N:1)
            builder.HasOne(p => p.Invoice)
                   .WithMany(i => i.Payments)
                   .HasForeignKey(p => p.InvoiceId)
                   .OnDelete(DeleteBehavior.Cascade); // Limpieza automática

            builder.ToTable("Payments");
        }
    }
}