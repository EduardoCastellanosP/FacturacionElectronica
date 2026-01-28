using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura.Api.DTOs
{
    public class ActualizarInvoiceDetalleDto
    {
        public ActualizarInvoiceDetalleDto()
        {
        }
        public ActualizarInvoiceDetalleDto(Guid? productoId, int? cantidad, decimal? precioUnitario, decimal? subtotal, decimal? total)
        {
            ProductoId = productoId;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
            Subtotal = Cantidad * PrecioUnitario;
            Total = Subtotal;
        }
        public Guid? ProductoId { get; set; }
        public int? Cantidad { get; set; }
        public decimal? PrecioUnitario { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? Total { get; set; }
    }
}