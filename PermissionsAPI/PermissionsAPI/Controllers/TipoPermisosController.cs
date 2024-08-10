using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PermissionsAPI.Data.Repositories;
using PermissionsAPI.DTOs;
using PermissionsAPI.Models;
using PermissionsAPI.Services.CQRS.Commands;
using System.Security;

[Route("api/[controller]")]
[ApiController]
public class TipoPermisosController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TipoPermisosController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // GET: api/TipoPermisos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TipoPermisoDto>>> GetAll()
    {
        var tipoPermisos = await _unitOfWork.TipoPermisoRepository.GetAllAsync();
        var tipoPermisosDto = _mapper.Map<IEnumerable<TipoPermisoDto>>(tipoPermisos);
        return Ok(tipoPermisosDto);
    }

    // POST: api/TipoPermisos
    [HttpPost]
    public async Task<ActionResult<CreateTipoPermisoDto>> Create([FromBody] RequestTipoPermisoDto requestDto)
    {
        var tipoPermiso = _mapper.Map<TipoPermiso>(requestDto);
        await _unitOfWork.TipoPermisoRepository.AddAsync(tipoPermiso);
        await _unitOfWork.CompleteAsync();
        return Ok(tipoPermiso.Id);
        //var permissionDto = _mapper.Map<CreateTipoPermisoDto>(tipoPermiso);

        //return CreatedAtAction(nameof(GetAll), new { id = permissionDto.Id }, permissionDto);
    }

    // PUT: api/TipoPermisos/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTipoPermisoDto updateTipoPermisoDto)
    {
        if (id != updateTipoPermisoDto.Id)
        {
            return BadRequest("El ID en la URL no coincide con el ID en el cuerpo de la solicitud.");
        }

        var tipoPermiso = await _unitOfWork.TipoPermisoRepository.GetByIdAsync(id);

        if (tipoPermiso == null)
        {
            return NotFound("Tipo de permiso no encontrado.");
        }


        _mapper.Map(updateTipoPermisoDto, tipoPermiso);
        _unitOfWork.TipoPermisoRepository.Update(tipoPermiso);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}
