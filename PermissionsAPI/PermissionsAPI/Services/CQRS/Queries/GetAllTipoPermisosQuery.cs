using MediatR;
using PermissionsAPI.DTOs;
using System.Collections.Generic;

public class GetAllTipoPermisosQuery : IRequest<IEnumerable<TipoPermisoDto>>
{
}