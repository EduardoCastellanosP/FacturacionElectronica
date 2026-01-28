using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Factura.Application.Abstractions;
using Factura.Infrastructure.Persistence;
using Factura.Infrastructure.Persistence.Repositories;

namespace Factura.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IClientesRepository? _clientesRepository;
        private IInvoiceRepository? _invoiceRepository;
        private IEmisorRepository? _emisorRepository;
        private IProductoRepository? _productosRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IClientesRepository Clientes => _clientesRepository ??= new ClienteRepository(_context);
        public IInvoiceRepository Invoices => _invoiceRepository ??= new InvoiceRepository(_context);
        public IEmisorRepository Emisores => _emisorRepository ??= new EmisorRepository(_context);
        public IProductoRepository Productos => _productosRepository ??= new ProductoRepository(_context);

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task ExecuteInTransactionAsync(Func<CancellationToken, Task> operation, CancellationToken cancellationToken = default)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                await operation(cancellationToken);
                await transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }
    }
}