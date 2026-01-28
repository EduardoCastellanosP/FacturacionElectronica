using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.src.Factura.Domain.Entities;

namespace Factura.Application.Abstractions
{
    public interface IEmisorRepository
    {
        Task<bool> ExistsEmisorAsync(Guid emisorId, CancellationToken cancellationToken = default);
        Task<Emisor?> GetEmisorByIdAsync(Guid emisorId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Emisor>> GetEmisorAllAsync(CancellationToken cancellationToken = default);
        void Add(Emisor emisor);
        void Update(Emisor emisor);
        void Delete(Emisor emisor);
    }
}