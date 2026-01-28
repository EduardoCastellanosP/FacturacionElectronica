using Backend.src.Factura.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Factura.Infrastructure.Persistence.Configurations
{
    public class DocumentoStatusConfiguration : IEntityTypeConfiguration<DocumentoStatus>
    {
        public void Configure(EntityTypeBuilder<DocumentoStatus> builder)
        {
            // Id heredada de BaseEntity
            builder.HasKey(e => e.Id);

            // El nombre del estado debe ser único para evitar confusión
            builder.Property(e => e.Estado)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(e => e.Estado)
                .IsUnique();

            // Configuración de la relación inversa (1:N)
            builder.HasMany(e => e.Invoices)
                .WithOne(i => i.DocumentoStatus) // Asegúrate que en Invoice se llame DocumentStatus
                .HasForeignKey(i => i.DocumentoStatusId)
                .OnDelete(DeleteBehavior.Restrict); // No borrar un estado si tiene facturas

            builder.ToTable("DocumentoStatuses");
        }
    }
}