using Newtonsoft.Json;

namespace PermissionsAPI.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidoEmpleado { get; set; }
        public int TipoPermisoId { get; set; }
        public DateTime FechaPermiso { get; set; }

        [JsonIgnore]
        public TipoPermiso TipoPermiso { get; set; } 
    }
}
