using Backend.src.Factura.Domain.Entities;
using Factura.Infrastructure.Persistence;

namespace Factura.Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // 1. Asegurar que la base de datos exista
            await context.Database.EnsureCreatedAsync();

            // 2. Insertar Estados de Documento si no existen
            if (!context.DocumentoStatuses.Any())
            {
                context.DocumentoStatuses.AddRange(
                    new DocumentoStatus { Id = Guid.NewGuid(), Estado = "Borrador", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new DocumentoStatus { Id = Guid.NewGuid(), Estado = "Emitida", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new DocumentoStatus { Id = Guid.NewGuid(), Estado = "Pagada", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
                    new DocumentoStatus { Id = Guid.NewGuid(), Estado = "Anulada", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
                );
            }

            // Insertar un Emisor por defecto para pruebas
            if (!context.Emisores.Any())
            {
                context.Emisores.Add(new Emisor
                {
                    Id = Guid.NewGuid(),
                    Nit = "900.123.456-1",
                    RazonSocial = "Mi Empresa de Facturación S.A.S",
                    Email = "facturacion@miempresa.com",
                    Direccion = "Calle Principal #123",
                    Telefono = "601234567",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                });
            }

            await context.SaveChangesAsync();
        }
    }
}