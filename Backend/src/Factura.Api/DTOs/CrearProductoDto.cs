using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura.Api.DTOs;

    public class CrearProductoDto
    {
    public CrearProductoDto()
    {
    }

    public CrearProductoDto(string? codigo, string? descripcion)
    {
        Codigo = codigo;
        Descripcion = descripcion;
    }

    public string? Codigo { get; set; }
    public string? Descripcion { get; set; }
}
