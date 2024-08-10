using Microsoft.EntityFrameworkCore;
using PermissionsAPI.DTOs;
using PermissionsAPI.Models;

namespace PermissionsAPI.Data.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly PermissionsDbContext _context;

        public PermissionRepository(PermissionsDbContext context)
        {
            _context = context;
        }

        public async Task<Permission> GetByIdAsync(int id) =>
            await _context.Permissions.Include(p => p.TipoPermiso).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<Permission>> GetAllAsync() =>
            await _context.Permissions.Include(p => p.TipoPermiso).ToListAsync();

        public async Task AddAsync(Permission permission)
        {
            await _context.Permissions.AddAsync(permission);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Permission permission)
        {
            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission != null)
            {
                _context.Permissions.Remove(permission);
                await _context.SaveChangesAsync();
            }
        }

        public void Update(Permission entity)
        {
            _context.Permissions.Update(entity); 
        }

        public void Remove(Permission entity)
        {
            _context.Permissions.Remove(entity);
        }

    }
}
