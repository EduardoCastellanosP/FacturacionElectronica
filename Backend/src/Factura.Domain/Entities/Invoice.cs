using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Factura.Domain.Entities;

namespace Backend.src.Factura.Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public Guid ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public Guid EmisorId { get; set; }
        public Emisor? Emisor { get; set; }

        public DateTime FechaEmision { get; set; }
        public decimal TotalFactura { get; set; }

        public Guid DocumentoStatusId { get; set; }
        public DocumentoStatus? DocumentoStatus { get; set; }

        public ICollection<DetalleFactura>? DetalleFacturas { get; set; }

        public ICollection<Payment>? Payments { get; set; }

    }
}