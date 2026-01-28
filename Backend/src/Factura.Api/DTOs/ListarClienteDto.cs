
namespace Factura.Api.DTOs;

    public class ListarClienteDto
    {
        public ListarClienteDto() { }
        public ListarClienteDto(Guid id, string nit, string nombres, string apellidos, string direccion, string telefono, string email)
        {
            Id = id;
            Nit = nit;
            Nombres = nombres;
            Apellidos = apellidos;
            Direccion = direccion;
            Telefono = telefono;
            Email = email;
        }
        public Guid Id { get; set; }
        public string Nit { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
    
