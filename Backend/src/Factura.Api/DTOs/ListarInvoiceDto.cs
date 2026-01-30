namespace Factura.Api.DTOs
{
    public class ListarInvoiceDto
    {

        public ListarInvoiceDto()
        {
        }
        
        public ListarInvoiceDto(Guid id, DateTime fechaEmision, decimal totalFactura, string? clienteNombre, string? emisorRazonSocial, string? estado, List<ListarInvoiceDetalleDto>? detalleFacturas)
        {
            Id = id;
            FechaEmision = fechaEmision;
            TotalFactura = totalFactura;
            ClienteNombre = clienteNombre;
            EmisorRazonSocial = emisorRazonSocial;
            Estado = estado;
            DetalleFacturas = detalleFacturas;
        }
        public Guid Id { get; set; }
        public DateTime FechaEmision { get; set; }
        public decimal TotalFactura { get; set; }
        public string? ClienteNombre { get; set; }
        public string? EmisorRazonSocial { get; set; }
        public string? Estado { get; set; }

        public List<ListarInvoiceDetalleDto>? DetalleFacturas { get; set; }
    }
}