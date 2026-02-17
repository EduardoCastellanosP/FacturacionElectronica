using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Backend.src.Factura.Domain.Entities;
using Factura.Api.DTOs;
using Factura.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Factura.Api.Controllers;

[EnableRateLimiting("ipLimiter")]
[ApiController]
[Route("api/[controller]")]
public class ProductoController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ProductoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    // 1. Obtener todos los productos (Para llenar listas o tablas en React)
    [HttpGet]
    public async Task<IActionResult> ObtenerProductos()
    {
        var productos = await _unitOfWork.Productos.ObtenerTodosProductos(); // Ajusta al nombre de tu método en el Repo
        var productosDto = _mapper.Map<IEnumerable<ObtenerProductoDto>>(productos);
        return Ok(productosDto);
    }

    // 2. Obtener por Código (Cambiado de Guid id a string codigo)
    [HttpGet("{codigo}", Name = "ObtenerProducto")]
    public async Task<IActionResult> ObtenerProductoByCodigo(string codigo)
    {
        // Nota: Asegúrate de tener este método en tu Repositorio
        var producto = await _unitOfWork.Productos.ObtenerProductoByCodigo(codigo); 
        if (producto == null) return NotFound(new { mensaje = "Producto no encontrado" });

        var productoDto = _mapper.Map<ObtenerProductoDto>(producto);
        return Ok(productoDto);
    }

    // 3. Crear Producto (Recibe Codigo y Descripcion desde React)
    [HttpPost]
    public async Task<ActionResult<CrearProductoDto>> CrearProductoAsync([FromBody] CrearProductoDto crearProductoDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Opcional: Validar si el código ya existe antes de crear
        var existe = await _unitOfWork.Productos.ExisteCodigoAsync(crearProductoDto.Codigo);
        if (existe) return BadRequest("El código de producto ya está registrado.");

        var producto = _mapper.Map<Producto>(crearProductoDto);
        
        await _unitOfWork.Productos.CrearProductoAsync(producto);
        await _unitOfWork.SaveChangesAsync();

        var productoDto = _mapper.Map<ObtenerProductoDto>(producto);
        
        // Retornamos la ruta usando el código como identificador
        return CreatedAtRoute("ObtenerProducto", new { codigo = producto.Codigo }, productoDto);
    }

    // 4. Actualizar por Código
    [HttpPut("{codigo}")]
    public async Task<IActionResult> ActualizarProducto(string codigo, [FromBody] ActualizarProductoDto actualizarProductoDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Buscamos por código en lugar de ID
        var productoExistente = await _unitOfWork.Productos.ObtenerProductoByCodigo(codigo);
        if (productoExistente == null) return NotFound();

        _mapper.Map(actualizarProductoDto, productoExistente);
        
        _unitOfWork.Productos.Update(productoExistente);
        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }
}