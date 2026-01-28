using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.src.Factura.Domain.Entities;
using Factura.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Factura.Infrastructure.Persistence.Repositories
{
    public sealed class ProductoRepository : IProductoRepository
    {
        private readonly AppDbContext _context;

        public ProductoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsProductoAsync(Guid productoId, CancellationToken cancellationToken = default)
        {
            return await _context.Productos.AnyAsync(p => p.Id == productoId, cancellationToken);
        }

        public async Task<Producto?> ObtenerProductoById(Guid productoId, CancellationToken cancellationToken = default)
        {
            return await _context.Productos.FindAsync(new object[] { productoId }, cancellationToken);
        }

        public async Task<IReadOnlyList<Producto>> ObtenerTodosProductos(CancellationToken cancellationToken = default)
        {
            return await _context.Productos.ToListAsync(cancellationToken);
        }

        public async Task<Producto?> CrearProductoAsync(Producto producto, CancellationToken ct = default)
        {
            var entityEntry = await _context.Productos.AddAsync(producto, ct);
            return entityEntry.Entity;
        }

        public void Add(Producto producto)
        {
            _context.Productos.Add(producto);
        }

        public void Update(Producto producto)
        {
            _context.Productos.Update(producto);
        }

        public void Delete(Producto producto)
        {
            _context.Productos.Remove(producto);
        }
    }
}