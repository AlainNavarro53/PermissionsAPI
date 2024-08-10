using System.Collections.Generic;
using System.Threading.Tasks;
using PermissionsAPI.Models;

public interface ITipoPermisoRepository
{
    Task<IEnumerable<TipoPermiso>> GetAllAsync();
    Task<TipoPermiso> GetByIdAsync(int id);
    Task AddAsync(TipoPermiso entity);
    void Update(TipoPermiso entity);
}
