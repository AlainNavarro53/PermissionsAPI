using MediatR;
using PermissionsAPI.Models;
using System;
using System.Collections.Generic;

public class GetPermissionsQuery : IRequest<IEnumerable<Permission>>
{
    public string NombreEmpleado { get; set; } 
    public string ApellidoEmpleado { get; set; } 
    public int? TipoPermisoId { get; set; } 
    public DateTime? FechaPermisoDesde { get; set; } 
    public DateTime? FechaPermisoHasta { get; set; } 
    public bool? Activo { get; set; }
}