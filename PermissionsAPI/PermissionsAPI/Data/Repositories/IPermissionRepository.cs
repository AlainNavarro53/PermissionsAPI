using PermissionsAPI.DTOs;
using PermissionsAPI.Models;

namespace PermissionsAPI.Data.Repositories
{
    public interface IPermissionRepository
    {
        Task<Permission> GetByIdAsync(int id);
        Task<IEnumerable<Permission>> GetAllAsync();
        Task AddAsync(Permission permission);
        Task UpdateAsync(Permission permission);
        Task DeleteAsync(int id);
        void Update(Permission entity);
        void Remove(Permission entity);

    }
}
