using Backend.src.Factura.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factura.Infrastructure.Persitence.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FechaEmision)
                .IsRequired();

            // Configuración de precisión para dinero (PostgreSQL: numeric(18,2))
            builder.Property(e => e.TotalFactura)
                .HasPrecision(18, 2)
                .IsRequired();

            // Relación con Cliente
            builder.HasOne(e => e.Cliente)
                .WithMany(c => c.Invoices)
                .HasForeignKey(e => e.ClienteId)
                .OnDelete(DeleteBehavior.Restrict); 

            // Relación con Emisor (Faltaba definirla explícitamente)
            builder.HasOne(e => e.Emisor)
                .WithMany(em => em.Invoices)
                .HasForeignKey(e => e.EmisorId)
                .IsRequired();

            // Relación con DocumentoStatus
            builder.HasOne(e => e.DocumentoStatus)
                .WithMany(s => s.Invoices)
                .HasForeignKey(e => e.DocumentoStatusId)
                .IsRequired();
                
            // Configuración de la tabla (opcional pero recomendado)
            builder.ToTable("Invoices");
        }
    }
}