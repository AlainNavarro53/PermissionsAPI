using MediatR;
using PermissionsAPI.Data.Repositories;
using PermissionsAPI.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GetPermissionsHandler : IRequestHandler<GetPermissionsQuery, IEnumerable<Permission>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPermissionsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Permission>> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
    {
        var query = _unitOfWork.PermissionRepository.GetAllAsync().Result.AsQueryable();

        if (!string.IsNullOrEmpty(request.NombreEmpleado))
        {
            query = query.Where(p => p.NombreEmpleado.Contains(request.NombreEmpleado));
        }

        if (!string.IsNullOrEmpty(request.ApellidoEmpleado))
        {
            query = query.Where(p => p.ApellidoEmpleado.Contains(request.ApellidoEmpleado));
        }

        if (request.TipoPermisoId.HasValue)
        {
            query = query.Where(p => p.TipoPermisoId == request.TipoPermisoId.Value);
        }

        if (request.FechaPermisoDesde.HasValue)
        {
            query = query.Where(p => p.FechaPermiso >= request.FechaPermisoDesde.Value);
        }

        if (request.FechaPermisoHasta.HasValue)
        {
            query = query.Where(p => p.FechaPermiso <= request.FechaPermisoHasta.Value);
        }

        return await Task.FromResult(query.ToList());
    }
}