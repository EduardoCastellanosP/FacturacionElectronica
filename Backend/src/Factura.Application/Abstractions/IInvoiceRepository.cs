using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.src.Factura.Domain.Entities;

namespace Factura.Application.Abstractions
{
    public interface IInvoiceRepository
    {
        Task<bool> ExistsInvoiceAsync(Guid invoiceId, CancellationToken cancellationToken = default);
        Task<Invoice?> GetInvoiceByIdAsync(Guid invoiceId,string? includeProperties = null,  CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Invoice>> GetInvoiceAllAsync(string? includeProperties = null, CancellationToken cancellationToken = default);        
        void Add(Invoice invoice);
        void Update(Invoice invoice);
        void Delete(Invoice invoice);
       
    }
}