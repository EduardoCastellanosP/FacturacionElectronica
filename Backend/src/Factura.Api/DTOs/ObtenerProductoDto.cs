using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura.Api.DTOs
{
    public class ObtenerProductoDto
    {
        public ObtenerProductoDto()
        {
        }
        public ObtenerProductoDto(int codigo, string? descripcion)
        {
            Codigo = codigo;
            Descripcion = descripcion;
        }
        public int Codigo { get; set; }
        public string? Descripcion { get; set; }
    }
}