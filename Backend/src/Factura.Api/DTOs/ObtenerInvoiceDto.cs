namespace Factura.Api.DTOs
{
    public class ObtenerInvoiceDto
    {
        public ObtenerInvoiceDto()
        {
        }

        public ObtenerInvoiceDto(Guid id, Guid clienteId, Guid emisorId, DateTime fechaEmision, decimal totalFactura, Guid documentoStatusId, List<ObtenerInvoiceDetalleDto> detalleFacturas)
        {
            Id = id;
            ClienteId = clienteId;
            EmisorId = emisorId;
            FechaEmision = fechaEmision;
            TotalFactura = totalFactura;
            DocumentoStatusId = documentoStatusId;
        }

        public Guid Id { get; set; } // Coincide con BaseEntity
        public Guid ClienteId { get; set; } // Agregado para integridad
        public Guid EmisorId { get; set; }
        public Guid DocumentoStatusId { get; set; } // Corregido el nombre
        public DateTime FechaEmision { get; set; }
        public decimal TotalFactura { get; set; }
        public List<ObtenerInvoiceDetalleDto> DetalleFacturas { get; set; } = new();
    }
}