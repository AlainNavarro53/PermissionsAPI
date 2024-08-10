using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PermissionsAPI.Data.Repositories;
using PermissionsAPI.DTOs;
using PermissionsAPI.Models;
using PermissionsAPI.Services.CQRS.Commands;

namespace PermissionsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        //private readonly IMediator _mediator;

        //public PermissionsController(IMediator mediator)
        //{
        //    _mediator = mediator;
        //}

        //[HttpPost("request")]
        //public async Task<ActionResult<Permission>> RequestPermission([FromBody] RequestPermissionCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    return Ok(result);
        //}

        //[HttpPut("modify/{id}")]
        //public async Task<ActionResult<Permission>> ModifyPermission(int id, [FromBody] ModifyPermissionCommand command)
        //{
        //    if (id != command.Id) return BadRequest();
        //    var result = await _mediator.Send(command);
        //    return Ok(result);
        //}

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Permission>>> GetPermissions()
        //{
        //    var query = new GetPermissionsQuery();
        //    var result = await _mediator.Send(query);
        //    return Ok(result);
        //}
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PermissionsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PermissionDto>>> GetPermissions()
        {
            var permissions = await _unitOfWork.PermissionRepository.GetAllAsync();
            var permissionsDto = _mapper.Map<IEnumerable<PermissionDto>>(permissions);
            return Ok(permissionsDto);
        }

        [HttpPut("modify/{id}")]
        public async Task<IActionResult> ModifyPermission(int id, [FromBody] ModifyPermissionDto modifyDto)
        {
            if (id != modifyDto.Id)
            {
                return BadRequest("El ID en la URL no coincide con el ID en el cuerpo de la solicitud.");
            }

            var permission = await _unitOfWork.PermissionRepository.GetByIdAsync(id);

            if (permission == null)
            {
                return NotFound("Permiso no encontrado.");
            }

            _mapper.Map(modifyDto, permission);

            _unitOfWork.PermissionRepository.Update(permission);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<PermissionDto>> RequestPermission([FromBody] RequestPermissionDto requestDto)
        {
            var permission = _mapper.Map<Permission>(requestDto);

            await _unitOfWork.PermissionRepository.AddAsync(permission);
            await _unitOfWork.CompleteAsync();

            var permissionDto = _mapper.Map<PermissionDto>(permission);

            return CreatedAtAction(nameof(GetPermissions), new { id = permissionDto.Id }, permissionDto);
        }
    }
}
