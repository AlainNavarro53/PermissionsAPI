using Newtonsoft.Json;

namespace PermissionsAPI.Models
{
    public class TipoPermiso
    {
        public int Id { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public ICollection<Permission> Permissions { get; set; }
    }
}
