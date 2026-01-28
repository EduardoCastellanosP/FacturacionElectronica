using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.src.Factura.Domain.Entities;
using Factura.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Factura.Infrastructure.Persistence.Repositories
{
    public sealed class ClienteRepository : IClientesRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cliente?> CrearClienteAsync(Cliente cliente, CancellationToken ct = default)
        {
            await _context.Clientes.AddAsync(cliente, ct);
            return cliente;
        }

        public async Task<bool> ExisteClienteAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Clientes.AnyAsync(c => c.Id == id, ct);
        }

        public async Task<Cliente?> ObtenerClientePorIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Clientes.FindAsync(new object[] { id }, ct);
        }

        public async Task<Cliente?> ObtenerClienteConFacturasAsync(Guid id, CancellationToken ct = default)
        {
            return await _context.Clientes
                .AsNoTracking() // Optimiza el rendimiento para consultas de solo lectura
                .Include(c => c.Invoices)
                .FirstOrDefaultAsync(c => c.Id == id, ct);
        }

        public async Task<IReadOnlyList<Cliente>> ObtenerTodosClientesAsync(CancellationToken ct = default)
        {
            return await _context.Clientes.AsNoTracking().ToListAsync(ct);
        }

        public async Task<Cliente?> GetClienteByIdAsync(Guid clienteId, CancellationToken cancellationToken = default)
        {
            return await _context.Clientes.FindAsync(new object[] { clienteId }, cancellationToken);
        }

        public async Task<Cliente> EliminarClienteAsync(Cliente cliente, CancellationToken ct = default)
        {
            _context.Clientes.Remove(cliente);
            return await Task.FromResult(cliente);
        }

        public void Add(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
        }

        public void Update(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
        }

        public void Delete(Cliente cliente)
        {
            _context.Clientes.Remove(cliente);
        }
    }
}