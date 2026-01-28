using System;
using System.Collections.Generic;
using System.Linq;
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

    [HttpGet("{id}", Name = "ObtenerProducto")]
    public async Task<IActionResult> ObtenerProductoById(Guid id)
    {
        var producto = await _unitOfWork.Productos.ObtenerProductoById(id);
        if (producto == null) return NotFound();

        var productoDto = _mapper.Map<ObtenerProductoDto>(producto);
        return Ok(productoDto);
    }

    [HttpPost]
    public async Task<ActionResult<CrearProductoDto>> CrearProductoAsync([FromBody] CrearProductoDto crearProductoDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var producto = _mapper.Map<Producto>(crearProductoDto);
        await _unitOfWork.Productos.CrearProductoAsync(producto);
        await _unitOfWork.SaveChangesAsync();

        var productoDto = _mapper.Map<ObtenerProductoDto>(producto);
        return CreatedAtRoute(nameof(ObtenerProductoById), new { id = producto.Id }, productoDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ActualizarProducto(Guid id, [FromBody] ActualizarProductoDto actualizarProductoDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var productoExistente = await _unitOfWork.Productos.ObtenerProductoById(id);
        if (productoExistente == null) return NotFound();

        _mapper.Map(actualizarProductoDto, productoExistente);
        _unitOfWork.Productos.Update(productoExistente);
        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }

}
