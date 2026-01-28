using Factura.Domain.Entities;


namespace Backend.src.Factura.Domain.Entities
{
    public class Emisor : BaseEntity
    {
        public string? Nit { get; set; }
        public string? RazonSocial { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }

        public ICollection<Invoice>? Invoices { get; set; }
    }
}