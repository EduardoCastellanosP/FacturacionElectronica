using System.Data;

namespace Factura.Api.DTOs
{
    public class CrearClienteDto
    {
        // Constructor vacío necesario para AutoMapper
        public CrearClienteDto() { }


        public CrearClienteDto(string? nit, string? nombres, string? apellidos, string? direccion, string? telefono, string? email)
        {
            Nit = nit;
            Nombres = nombres;
            Apellidos = apellidos;
            Direccion = direccion;
            Telefono = telefono;
            Email = email;
        }
        public string? Nit { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }

    }
}