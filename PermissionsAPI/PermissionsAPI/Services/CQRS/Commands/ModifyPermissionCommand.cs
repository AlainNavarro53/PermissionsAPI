using MediatR;
using PermissionsAPI.Models;
using System;

public class ModifyPermissionCommand : IRequest<Permission>
{
    public int Id { get; set; }
    public string NombreEmpleado { get; set; }
    public string ApellidoEmpleado { get; set; }
    public int TipoPermisoId { get; set; }
    public DateTime FechaPermiso { get; set; }
}