using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Factura.Domain.Entities;

namespace Backend.src.Factura.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid InvoiceId { get; set; }
        public DateTime FechaPago { get; set; }
        public required string MetodoPago { get; set; }

        public Invoice? Invoice { get; set; }
        
    }
}