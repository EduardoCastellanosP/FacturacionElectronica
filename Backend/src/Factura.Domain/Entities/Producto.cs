using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Factura.Domain.Entities;

namespace Backend.src.Factura.Domain.Entities
{
    public class Producto : BaseEntity
    {
        public string? Codigo { get; set; }
        public string? Descripcion { get; set; }
    }
}