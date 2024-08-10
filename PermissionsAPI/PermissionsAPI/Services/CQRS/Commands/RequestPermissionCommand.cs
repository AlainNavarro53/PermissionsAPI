using MediatR;
using PermissionsAPI.Models;

namespace PermissionsAPI.Services.CQRS.Commands
{
    public class RequestPermissionCommand : IRequest<Permission>
    {
        public string NombreEmpleado { get; set; }
        public string ApellidoEmpleado { get; set; }
        public int TipoPermisoId { get; set; }
        public DateTime FechaPermiso { get; set; }
    }
}
