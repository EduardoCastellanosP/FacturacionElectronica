namespace Factura.Api.DTOs
{
    public class ListarInvoiceDto
    {
        
        public ListarInvoiceDto()
        {
        }

        public Guid Id { get; set; }
        public DateTime FechaEmision { get; set; }
        public decimal TotalFactura { get; set; }
        public string? ClienteNombre { get; set; }
        public string? EmisorRazonSocial { get; set; }
        public string? Estado { get; set; }
    }
}