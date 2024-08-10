namespace PermissionsAPI.DTOs
{
    public class PermissionDto
    {
        public int Id { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidoEmpleado { get; set; }
        public int TipoPermisoId { get; set; }
        public string TipoPermisoDescripcion { get; set; }
        public DateTime FechaPermiso { get; set; }
    }
}
