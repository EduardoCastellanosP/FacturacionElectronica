using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.src.Factura.Domain.Entities;
using Factura.Application.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Factura.Infrastructure.Persistence.Repositories
{
    public sealed class EmisorRepository : IEmisorRepository
    {
        private readonly AppDbContext _context;

        public EmisorRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ExistsEmisorAsync(Guid emisorId, CancellationToken cancellationToken = default)
        {
            return await _context.Emisores.AnyAsync(e => e.Id == emisorId, cancellationToken);
        }

        public async Task<Emisor?> GetEmisorByIdAsync(Guid emisorId, CancellationToken cancellationToken = default)
        {
            return await _context.Emisores.FindAsync(new object[] { emisorId }, cancellationToken);
        }

        public async Task<IReadOnlyList<Emisor>> GetEmisorAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Emisores.AsNoTracking().ToListAsync(cancellationToken);
        }

        public void Add(Emisor emisor)
        {
            _context.Emisores.Add(emisor);
        }

        public void Update(Emisor emisor)
        {
            _context.Emisores.Update(emisor);
        }

        public void Delete(Emisor emisor)
        {
            _context.Emisores.Remove(emisor);
        }
    }
}