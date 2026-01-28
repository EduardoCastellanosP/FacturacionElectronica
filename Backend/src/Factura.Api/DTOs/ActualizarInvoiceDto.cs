using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura.Api.DTOs
{
    public class ActualizarInvoiceDto
    {
        public ActualizarInvoiceDto()
        {
        }

        public ActualizarInvoiceDto(Guid clienteId, Guid emisorId, Guid documentoStatusId, DateTime? fechaEmision, decimal? totalFactura)
        {
            ClienteId = clienteId;
            EmisorId = emisorId;
            DocumentoStatusId = documentoStatusId;
            FechaEmision = fechaEmision;
            TotalFactura = totalFactura;
        }

        public Guid ClienteId { get; set; }
        public Guid EmisorId { get; set; }
        public Guid DocumentoStatusId { get; set; }
        public DateTime? FechaEmision { get; set; }
        public decimal? TotalFactura { get; set; }
    }
}