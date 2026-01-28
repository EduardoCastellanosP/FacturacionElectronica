using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura.Api.DTOs
{
    public class ObtenerInvoiceDetalleDto
    {
        public ObtenerInvoiceDetalleDto()
        {
        }
        public ObtenerInvoiceDetalleDto(Guid id, Guid invoiceId, int productoCodigo, string? productoDescripcion, int cantidad, decimal precioUnitario)
        {
            Id = id;
            InvoiceId = invoiceId;
            ProductoCodigo = productoCodigo;
            ProductoDescripcion = productoDescripcion;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
        }

        public Guid Id { get; set; }
        public Guid InvoiceId { get; set; }
        public int ProductoCodigo { get; set; }
        public string? ProductoDescripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}