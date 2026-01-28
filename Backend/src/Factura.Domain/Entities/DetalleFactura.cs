using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Factura.Domain.Entities;

namespace Backend.src.Factura.Domain.Entities
{
    public class DetalleFactura : BaseEntity
    {
        public Guid InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }

        public Guid ProductoId { get; set; }
        public Producto? Producto { get; set; }

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }


    }
}