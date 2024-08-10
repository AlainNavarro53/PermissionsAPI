using MediatR;
using PermissionsAPI.Data.Repositories;
using PermissionsAPI.Models;
using PermissionsAPI.Services.CQRS.Commands;

namespace PermissionsAPI.Services.CQRS.Handlers
{
    public class RequestPermissionHandler : IRequestHandler<RequestPermissionCommand, Permission>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RequestPermissionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Permission> Handle(RequestPermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = new Permission
            {
                NombreEmpleado = request.NombreEmpleado,
                ApellidoEmpleado = request.ApellidoEmpleado,
                TipoPermisoId = request.TipoPermisoId,
                FechaPermiso = request.FechaPermiso
            };

            await _unitOfWork.PermissionRepository.AddAsync(permission);
            await _unitOfWork.CompleteAsync();

            return permission;
        }
    }
}
