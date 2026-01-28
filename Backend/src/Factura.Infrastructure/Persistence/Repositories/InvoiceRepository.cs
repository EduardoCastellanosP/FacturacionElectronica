using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.src.Factura.Domain.Entities;
using Factura.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Factura.Infrastructure.Persistence.Repositories
{
    public sealed class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;

        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default)
        {
            return await _context.Invoices.AnyAsync(i => i.Id == invoiceId, cancellationToken);
        }

      
        public async Task<IReadOnlyList<Invoice>> GetInvoiceAllAsync(string? includeProperties = null, CancellationToken ct = default)
        {
            IQueryable<Invoice> query = _context.Invoices;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                // Esto limpia espacios y separa por comas
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp.Trim());
                }
            }

            return await query.ToListAsync(ct);
        }

       public async Task<Invoice?> GetInvoiceByIdAsync(Guid invoiceId, string? includeProperties = null, CancellationToken cancellationToken = default)
        {
            IQueryable<Invoice> query = _context.Invoices;

            // Procesamos los includes (Cliente, Emisor, DetalleFacturas, etc.)
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp.Trim());
                }
            }

            // Buscamos la factura específica por su ID
            return await query.FirstOrDefaultAsync(x => x.Id == invoiceId, cancellationToken);
        }

        public void Add(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
        }

        public void Update(Invoice invoice)
        {
            _context.Invoices.Update(invoice);
        }

        public void Delete(Invoice invoice)
        {
            _context.Invoices.Remove(invoice);
        }
    }
}