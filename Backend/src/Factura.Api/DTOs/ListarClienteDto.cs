
namespace Factura.Api.DTOs;

    public record  ListarClienteDto(
        Guid Id,
        string Nombre,
        string Apellido,
        string Direccion,
        string Telefono,
        string Email
    );
    
        
    
