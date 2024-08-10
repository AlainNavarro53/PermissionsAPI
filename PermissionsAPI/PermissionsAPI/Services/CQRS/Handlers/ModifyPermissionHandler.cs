using MediatR;
using PermissionsAPI.Data.Repositories;
using PermissionsAPI.Models;

namespace PermissionsAPI.Services.CQRS.Handlers
{
    public class ModifyPermissionHandler : IRequestHandler<ModifyPermissionCommand, Permission>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ModifyPermissionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Permission> Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = await _unitOfWork.PermissionRepository.GetByIdAsync(request.Id);

            if (permission == null)
            {
                throw new KeyNotFoundException("Permission not found");
            }

            permission.NombreEmpleado = request.NombreEmpleado;
            permission.ApellidoEmpleado = request.ApellidoEmpleado;
            permission.TipoPermisoId = request.TipoPermisoId;
            permission.FechaPermiso = request.FechaPermiso;

            await _unitOfWork.PermissionRepository.UpdateAsync(permission);
            await _unitOfWork.CompleteAsync();

            return permission;
        }
    }
}
