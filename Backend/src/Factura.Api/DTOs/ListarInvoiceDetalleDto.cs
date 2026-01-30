using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura.Api.DTOs
{
    public class ListarInvoiceDetalleDto
    {

        public ListarInvoiceDetalleDto()
        {
        }

        public ListarInvoiceDetalleDto(Guid id, Guid productoId, int cantidad, decimal precioUnitario)
        {
            Id = id;
            ProductoId = productoId;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
        }


        public Guid Id { get; set; }
    public Guid ProductoId { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}