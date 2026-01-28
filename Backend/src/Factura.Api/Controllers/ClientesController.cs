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
public class ClientesController : ControllerBase
{
    // Aquí puedes agregar tus acciones (endpoints) para el controlador de clientes
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ClientesController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    // Aquí puedes agregar tus acciones (endpoints) para el controlador de clientes

    [HttpGet("{id}", Name = "ObtenerCliente")]
    public async Task<ActionResult<ObtenerClienteDetalleDto>> ObtenerCliente(Guid id)
    {
        var cliente = await _unitOfWork.Clientes.ObtenerClientePorIdAsync(id);
        if (cliente == null) return NotFound();

        var clienteDto = _mapper.Map<ObtenerClienteDetalleDto>(cliente);
        return Ok(clienteDto);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ListarClienteDto>>> ListarClientes()
    {
        var clientes = await _unitOfWork.Clientes.ObtenerTodosClientesAsync();

        var clientesDto = _mapper.Map<IEnumerable<ListarClienteDto>>(clientes);

        return Ok(clientesDto);
    }


    [HttpPost]
    public async Task<ActionResult<CrearClienteDto>> CrearClienteAsync(CrearClienteDto crearClienteDto)
    {
        var cliente = _mapper.Map<Cliente>(crearClienteDto);
        _unitOfWork.Clientes.Add(cliente);
        await _unitOfWork.SaveChangesAsync();

        var clienteDto = _mapper.Map<CrearClienteDto>(cliente);
        return CreatedAtAction(nameof(ObtenerCliente), new { id = cliente.Id }, clienteDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ActualizarClienteDto>> ActualizarClienteAsync(Guid id, ActualizarClienteDto actualizarClienteDto)
    {
        var cliente = await _unitOfWork.Clientes.ObtenerClientePorIdAsync(id);
        if (cliente == null) return NotFound();

        _mapper.Map(actualizarClienteDto, cliente);
        _unitOfWork.Clientes.Update(cliente);
        await _unitOfWork.SaveChangesAsync();

        var clienteDto = _mapper.Map<ActualizarClienteDto>(cliente);
        return Ok(clienteDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarClienteAsync(Guid id)
    {
        var cliente = await _unitOfWork.Clientes.ObtenerClientePorIdAsync(id);
        if (cliente == null) return NotFound();

        _unitOfWork.Clientes.Delete(cliente);
        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }

}
