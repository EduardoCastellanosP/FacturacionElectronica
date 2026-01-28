using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Factura.Domain.Entities;

namespace Backend.src.Factura.Domain.Entities
{
    public class DocumentoStatus : BaseEntity
    {
        public Guid DocumentoId { get; set; }
        public required string Estado { get; set; }

        public ICollection<Invoice>? Invoices { get; set; }

    }
}