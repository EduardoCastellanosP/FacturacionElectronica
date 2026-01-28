using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.src.Factura.Domain.Entities;

namespace Factura.Application.Abstractions
{
    public interface IClientesRepository
    {
       // LECTURAS (Async porque van a la DB)
    Task<Cliente?>CrearClienteAsync(Cliente cliente, CancellationToken ct = default);
    Task<bool> ExisteClienteAsync(Guid id, CancellationToken ct = default);
    Task<Cliente?> ObtenerClientePorIdAsync(Guid id, CancellationToken ct = default);
    Task<Cliente?> ObtenerClienteConFacturasAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<Cliente>> ObtenerTodosClientesAsync(CancellationToken ct = default);
    Task <Cliente>EliminarClienteAsync(Cliente cliente, CancellationToken ct = default);

    // ESCRITURAS (Sincrónicas en el Repositorio)
    void Add(Cliente cliente);
    void Update(Cliente cliente);
    void Delete(Cliente cliente);

    }
}