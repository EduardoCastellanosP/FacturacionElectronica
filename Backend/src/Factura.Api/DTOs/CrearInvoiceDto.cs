namespace Factura.Api.DTOs;

public class CrearInvoiceDetalleDto
{
    public string? ProductoId { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}

public class CrearInvoiceDto
{
    public string? ClienteId { get; set; }
    public string? EmisorId { get; set; }
    public DateTime FechaEmision { get; set; }
    public decimal TotalFactura { get; set; }
    public string? DocumentoStatusId { get; set; }
    // Asegúrate de que este nombre coincida con el del JSON
    public List<CrearInvoiceDetalleDto> DetalleFacturas { get; set; } = new();
}