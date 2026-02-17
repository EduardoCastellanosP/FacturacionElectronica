using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.src.Factura.Domain.Entities;

namespace Factura.Application.Abstractions
{
    public interface IProductoRepository
    {
        Task<bool> ExisteCodigoAsync(string codigo, CancellationToken cancellationToken = default);
        Task<Producto?> ObtenerProductoByCodigo(string codigo, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Producto>> ObtenerTodosProductos(CancellationToken cancellationToken = default);
        Task<Producto?> CrearProductoAsync(Producto producto, CancellationToken ct = default);
        void Add(Producto producto);
        void Update(Producto producto);
        void Delete(Producto producto);
    }
}