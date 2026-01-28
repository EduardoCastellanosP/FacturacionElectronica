using System.Data;

namespace Factura.Api.DTOs
{
    public class CrearClienteDto
    {
        // Constructor vacío necesario para AutoMapper
        public CrearClienteDto() { }


        public CrearClienteDto(string? nombre, string? apellido)
        {
            Nombres = nombre;
            Apellidos = apellido;
        }

        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }

    }
}