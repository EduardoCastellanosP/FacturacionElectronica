using AutoMapper;
using Backend.src.Factura.Domain.Entities;
using Factura.Api.DTOs;

namespace Factura.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // --- 1. DETALLE FACTURA ---
            // Usamos ReverseMap para que funcione de DTO -> Entidad y viceversa
            CreateMap<CrearInvoiceDetalleDto, DetalleFactura>().ReverseMap();
            CreateMap<ActualizarInvoiceDetalleDto, DetalleFactura>().ReverseMap();

            CreateMap<DetalleFactura, ListarInvoiceDetalleDto>();
            CreateMap<DetalleFactura, ObtenerInvoiceDetalleDto>()
                .ForMember(dest => dest.ProductoDescripcion,
                    opt => opt.MapFrom(src => src.Producto != null ? src.Producto.Descripcion : "Sin descripción"));

            // --- 2. INVOICE (FACTURA) ---
            CreateMap<CrearInvoiceDto, Invoice>()
                .ForMember(dest => dest.Cliente, opt => opt.Ignore())
                .ForMember(dest => dest.Emisor, opt => opt.Ignore())
                .ForMember(dest => dest.DocumentoStatus, opt => opt.Ignore())
                .ForMember(dest => dest.DetalleFacturas, opt => opt.MapFrom(src => src.DetalleFacturas))
                .ReverseMap(); // IMPORTANTE: Para que el controlador pueda devolver la factura creada

            CreateMap<ActualizarInvoiceDto, Invoice>().ReverseMap();
            CreateMap<Invoice, ObtenerInvoiceDto>();
            
            // Mapeo para la tabla de React (incluye navegación)
            CreateMap<Invoice, ListarInvoiceDto>()
                .ForMember(dest => dest.ClienteNombre,
                    opt => opt.MapFrom(src => src.Cliente != null ? $"{src.Cliente.Nombres} {src.Cliente.Apellidos}" : "Cliente no asignado"))
                .ForMember(dest => dest.EmisorRazonSocial,
                    opt => opt.MapFrom(src => src.Emisor != null ? src.Emisor.RazonSocial : "Emisor no asignado"))
                .ForMember(dest => dest.Estado,
                    opt => opt.MapFrom(src => src.DocumentoStatus != null ? src.DocumentoStatus.Estado : "Estado no asignado"));

            // --- 3. CLIENTE ---
            CreateMap<Cliente, ListarClienteDto>();
            CreateMap<Cliente, CrearClienteDto>().ReverseMap();
            CreateMap<ActualizarClienteDto, Cliente>().ReverseMap();
            CreateMap<Cliente, ObtenerClienteDetalleDto>()
                .ForMember(dest => dest.Nombres, opt => opt.MapFrom(src => $"{src.Nombres} {src.Apellidos}"));

            // --- 4. EMISOR ---
            CreateMap<Emisor, ListarEmisorDto>();
            CreateMap<Emisor, CrearEmisorDto>().ReverseMap();
            CreateMap<Emisor, ObtenerEmisorDto>();
            CreateMap<ActualizarEmisorDto, Emisor>().ReverseMap();

            // --- 5. PRODUCTO ---
            CreateMap<Producto, ObtenerProductoDto>();
            CreateMap<CrearProductoDto, Producto>().ReverseMap();
            CreateMap<ActualizarProductoDto, Producto>().ReverseMap();
        }
    }
}