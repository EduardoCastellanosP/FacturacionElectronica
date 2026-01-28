
namespace Factura.Api.DTOs;


public class ObtenerClienteDetalleDto
{
    public ObtenerClienteDetalleDto()
    {
    }

    public ObtenerClienteDetalleDto(Guid id, string nombres, string apellidos, string direccion, string telefono, string email)
    {
        Id = id;
        Nombres = nombres;
        Apellidos = apellidos;
        Direccion = direccion;
        Telefono = telefono;
        Email = email;
    }

    public Guid Id { get; set; }
    public string Nombres { get; set; } = null!;
    public string Apellidos { get; set; } = null!;
    public string Direccion { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string Email { get; set; } = null!;
}
