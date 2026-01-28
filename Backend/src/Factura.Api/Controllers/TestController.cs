using Factura.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Factura.Domain.Entities;
using Backend.src.Factura.Domain.Entities;

namespace Factura.Api.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    private readonly AppDbContext _context;

    public TestController(AppDbContext context)
    {
        _context = context;
    }

   [HttpPost("invoice")]
public async Task<IActionResult> CreateInvoice()
{
    // 1️⃣ Crear Cliente
    var cliente = new Cliente
    {
        Nombres = "Cliente Test"
    };

    _context.Clientes.Add(cliente);
    await _context.SaveChangesAsync();

    // 2️⃣ Crear Emisor
    var emisor = new Emisor
    {
        Id = Guid.NewGuid(),
        Nit = "900123456-7",   // 👈 OBLIGATORIO
        RazonSocial = "Empresa X",
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow,
        Email = "emisor@test.com"


    };

    _context.Emisores.Add(emisor);
    await _context.SaveChangesAsync();

    // 3️⃣ Crear Invoice con FKs válidas
    var invoice = new Invoice
    {
        ClienteId = cliente.Id,
        EmisorId = emisor.Id,
        FechaEmision = DateTime.UtcNow,
        TotalFactura = 150000
    };

    _context.Invoices.Add(invoice);
    await _context.SaveChangesAsync();

    return Ok(invoice);
}
}