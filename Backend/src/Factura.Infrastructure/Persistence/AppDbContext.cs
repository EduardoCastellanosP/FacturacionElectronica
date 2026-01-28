using System.Reflection.Emit;
using Backend.src.Factura.Domain.Entities;
using Factura.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Factura.Infrastructure.Persistence;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{


    public DbSet<Emisor> Emisores => Set<Emisor>();
    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<DetalleFactura> DetalleFacturas => Set<DetalleFactura>();
    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Payment> Payments => Set<Payment>();

    public DbSet<DocumentoStatus> DocumentoStatuses => Set<DocumentoStatus>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
