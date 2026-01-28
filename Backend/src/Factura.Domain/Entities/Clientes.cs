using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Factura.Domain.Entities;

namespace Backend.src.Factura.Domain.Entities
{
    public class Cliente : BaseEntity
    {
        public string? Nit { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }

        public ICollection<Invoice>? Invoices { get; set; }
    }
}