using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Backend.src.Factura.Domain.Entities;
using Factura.Api.DTOs;
using Factura.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Factura.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public InvoiceController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ListarInvoiceDto>>> ObtenerInvoices()
    {
        var invoices = await _unitOfWork.Invoices
            .GetInvoiceAllAsync(includeProperties: "Cliente,Emisor,DocumentoStatus");

        var invoicesDto = _mapper.Map<IEnumerable<ListarInvoiceDto>>(invoices);

        return Ok(invoicesDto);
    }

    // HE UNIFICADO LOS DOS GET POR ID EN ESTE SOLO
    [HttpGet("{id}", Name = "GetInvoice")]
    public async Task<ActionResult<ObtenerInvoiceDto>> GetInvoice(Guid id)
    {
        // 1. Buscamos con TODOS los includes necesarios para que no salgan nulos
        var invoice = await _unitOfWork.Invoices
            .GetInvoiceByIdAsync(id, includeProperties: "Cliente,Emisor,DocumentoStatus,DetalleFacturas");
        
        if (invoice == null) return NotFound();

        // 2. Mapeamos al DTO de salida
        var invoiceDto = _mapper.Map<ObtenerInvoiceDto>(invoice);
        return Ok(invoiceDto);
    }

    [HttpPost]
    public async Task<ActionResult> CreateInvoice([FromBody] CrearInvoiceDto invoiceDto)
    {
        if (invoiceDto == null) return BadRequest();

        var invoiceEntity = _mapper.Map<Invoice>(invoiceDto);

        _unitOfWork.Invoices.Add(invoiceEntity);
        await _unitOfWork.SaveChangesAsync();

        var resultDto = _mapper.Map<ObtenerInvoiceDto>(invoiceEntity);

        return CreatedAtAction(nameof(GetInvoice), new { id = invoiceEntity.Id }, resultDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> ActualizarInvoiceAsync(Guid id, ActualizarInvoiceDto actualizarInvoiceDto)
    {
        var invoice = await _unitOfWork.Invoices.GetInvoiceByIdAsync(id);
        if (invoice == null) return NotFound();

        _mapper.Map(actualizarInvoiceDto, invoice);
        invoice.UpdatedAt = DateTime.UtcNow;
        _unitOfWork.Invoices.Update(invoice);
        await _unitOfWork.SaveChangesAsync();

        return NoContent(); 
    }
}