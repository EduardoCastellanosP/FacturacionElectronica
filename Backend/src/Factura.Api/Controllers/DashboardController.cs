using Factura.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetStats()
    {
        // 1. Contar cuántos emisores hay (o clientes)
        var totalEmisores = await _context.Emisores.CountAsync();

        // 2. Aquí podrías contar facturas si ya tienes la tabla
        // var totalFacturas = await _context.Facturas.CountAsync();

        return Ok(new {
            ventasTotales = 0, // Por ahora 0 hasta que hagamos la lógica de facturas
            facturasEmitidas = 0,
            clientesNuevos = totalEmisores, // Usamos los emisores como prueba
            graficoVentas = new[] {
                new { mes = "Ene", monto = 0 },
                new { mes = "Feb", monto = 0 }
            }
        });
    }
}