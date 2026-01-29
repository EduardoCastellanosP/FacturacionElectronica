using AutoMapper;
using Backend.src.Factura.Domain.Entities;
using Factura.Api.DTOs;

namespace Factura.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // --- CLIENTE ---
            CreateMap<Cliente, ListarClienteDto>();

            CreateMap<Cliente, ObtenerClienteDetalleDto>().ForMember(dest => dest.Nombres, opt => opt.MapFrom(src => $"{src.Nombres} {src.Apellidos}"));
            CreateMap<ActualizarClienteDto, Cliente>().ReverseMap(); // Para el PUT

            // --- INVOICE (FACTURA) ---
            CreateMap<Invoice, ObtenerInvoiceDto>();
            CreateMap<Invoice, ListarInvoiceDto>();

            CreateMap<CrearInvoiceDto, Invoice>();
            CreateMap<ActualizarInvoiceDto, Invoice>(); // Para el PUT
            CreateMap<CrearInvoiceDetalleDto, DetalleFactura>();
            CreateMap<Invoice, ListarInvoiceDto>()
            // De Cliente (que ya vimos que tiene Nombres y Apellidos)
            .ForMember(dest => dest.ClienteNombre, 
                    opt => opt.MapFrom(src => $"{src.Cliente.Nombres} {src.Cliente.Apellidos}"))
            
            // De Emisor (usando la propiedad RazonSocial que me acabas de pasar)
            .ForMember(dest => dest.EmisorRazonSocial, 
                    opt => opt.MapFrom(src => src.Emisor != null ? src.Emisor.RazonSocial : "EL OBJETO EMISOR LLEGÓ NULO"))
            
            // De DocumentoStatus (usando la propiedad Estado)
            .ForMember(dest => dest.Estado, 
                    opt => opt.MapFrom(src => src.DocumentoStatus != null ? src.DocumentoStatus.Estado : "EL OBJETO DOCUMENTO STATUS LLEGÓ NULO"));

            // --- DETALLE FACTURA ---
            CreateMap<DetalleFactura, ObtenerInvoiceDetalleDto>().ForMember(dest => dest.ProductoDescripcion, opt => opt.MapFrom(src => src.Producto != null ? src.Producto.Descripcion : "Producto sin nombre"));
            CreateMap<ActualizarInvoiceDetalleDto, DetalleFactura>(); // Para el PUT

            // --- PRODUCTO ---
            CreateMap<Producto, ObtenerProductoDto>();
            CreateMap<CrearProductoDto, Producto>();
            CreateMap<ActualizarProductoDto, Producto>(); // Para el PUT

            // --- EMISOR ---
            CreateMap<Emisor, CrearEmisorDto>().ReverseMap();
            CreateMap<Emisor, ListarEmisorDto>();
            CreateMap<Emisor, ObtenerEmisorDto>();
            CreateMap<ActualizarEmisorDto, Emisor>(); // Mapea RazonSocial automáticamente
            
            // --- REVERSE MAPS (Opcionales pero útiles para respuestas) ---
            CreateMap<Cliente, CrearClienteDto>().ReverseMap();
            CreateMap<Invoice, CrearInvoiceDto>().ReverseMap();
            CreateMap<DetalleFactura, CrearInvoiceDetalleDto>().ReverseMap();
            CreateMap<Emisor, CrearEmisorDto>().ReverseMap();
        }
    }
}