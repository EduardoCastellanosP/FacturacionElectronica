using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura.Api.DTOs
{
    public class ActualizarClienteDto
    {
        public ActualizarClienteDto()
        {
        }
        public ActualizarClienteDto(string? nombres, string? apellidos, string? direccion, string? telefono, string? email)
        {
            Nombres = nombres;
            Apellidos = apellidos;
            Direccion = direccion;
            Telefono = telefono;
            Email = email;
        }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }  
    }
}