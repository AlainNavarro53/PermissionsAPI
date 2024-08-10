using Microsoft.EntityFrameworkCore;
using PermissionsAPI.Data;
using PermissionsAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TipoPermisoRepository : ITipoPermisoRepository
{
    private readonly PermissionsDbContext _context;

    public TipoPermisoRepository(PermissionsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TipoPermiso>> GetAllAsync()
    {
        return await _context.TipoPermisos.ToListAsync();
    }

    public async Task<TipoPermiso> GetByIdAsync(int id)
    {
        return await _context.TipoPermisos.FindAsync(id);
    }

    public async Task AddAsync(TipoPermiso entity)
    {
        await _context.TipoPermisos.AddAsync(entity);
    }

    public void Update(TipoPermiso entity)
    {
        _context.TipoPermisos.Update(entity);
    }
}
