namespace PermissionsAPI.DTOs
{
    public class ModifyPermissionDto
    {
        public int Id { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidoEmpleado { get; set; }
        public int TipoPermisoId { get; set; }
        public DateTime FechaPermiso { get; set; }
        public bool Activo { get; set; }
    }
}
