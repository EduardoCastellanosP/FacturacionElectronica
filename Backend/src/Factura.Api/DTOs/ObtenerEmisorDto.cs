using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Factura.Api.DTOs
{
    public class ObtenerEmisorDto
    {
        public ObtenerEmisorDto()
        {
        }

        public ObtenerEmisorDto(Guid id, string? nit, string? razonSocial, string? direccion, string? telefono, string? email)
        {
            Id = id;
            Nit = nit;
            RazonSocial = razonSocial;
            Direccion = direccion;
            Telefono = telefono;
            Email = email;
        }
        public Guid Id { get; set; }
        public String? Nit { get; set; }
        public string? RazonSocial { get; set; }    
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
    }
}