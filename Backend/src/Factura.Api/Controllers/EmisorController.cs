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

public class EmisorController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public EmisorController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("{id}", Name = "GetEmisorById")]
    public async Task<ActionResult> GetEmisorByIdAsync(Guid id)
    {
        var emisor = await _unitOfWork.Emisores.GetEmisorByIdAsync(id);
        if (emisor == null) return NotFound();

        var emisorDto = _mapper.Map<ObtenerEmisorDto>(emisor);
        return Ok(emisorDto);
    }

    [HttpGet]
        public async Task<ActionResult<IEnumerable<ListarEmisorDto>>> ListarEmisores()
        {
            var emisores = await _unitOfWork.Emisores.GetEmisorAllAsync();

            var emisoresDto = _mapper.Map<IEnumerable<ListarEmisorDto>>(emisores);

            return Ok(emisoresDto);
        }


    [HttpPost]
        public async Task<ActionResult> CreateEmisorAsync([FromBody] CrearEmisorDto crearEmisorDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var emisor = _mapper.Map<Emisor>(crearEmisorDto);
            _unitOfWork.Emisores.Add(emisor);
            await _unitOfWork.SaveChangesAsync();

            var emisorDto = _mapper.Map<ObtenerEmisorDto>(emisor);

            // Usamos el nombre que definiste en el [HttpGet]
            return CreatedAtRoute("GetEmisorById", new { id = emisorDto.Id }, emisorDto);
        }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateEmisorAsync(Guid id, [FromBody] ActualizarEmisorDto actualizarEmisorDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var emisor = await _unitOfWork.Emisores.GetEmisorByIdAsync(id);
        if (emisor == null) return NotFound();

        _mapper.Map(actualizarEmisorDto, emisor);
        _unitOfWork.Emisores.Update(emisor);
        await _unitOfWork.SaveChangesAsync();

        var emisorDto = _mapper.Map<ObtenerEmisorDto>(emisor);
        return Ok(emisorDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEmisorAsync(Guid id)
    {
        var emisor = await _unitOfWork.Emisores.GetEmisorByIdAsync(id);
        if (emisor == null) return NotFound();

        _unitOfWork.Emisores.Delete(emisor);
        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }

}
