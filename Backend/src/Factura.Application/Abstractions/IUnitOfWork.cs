using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura.Application.Abstractions
{
    public interface IUnitOfWork
    {

        IClientesRepository Clientes { get; }
        IInvoiceRepository Invoices { get; }
        IEmisorRepository Emisores { get; }
        IProductoRepository Productos { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task ExecuteInTransactionAsync(Func<CancellationToken, Task> operation, CancellationToken cancellationToken = default);
    }
}