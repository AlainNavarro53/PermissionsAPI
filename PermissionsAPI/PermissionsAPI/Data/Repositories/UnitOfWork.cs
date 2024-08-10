namespace PermissionsAPI.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PermissionsDbContext _context;

        public UnitOfWork(PermissionsDbContext context)
        {
            _context = context;
            TipoPermisoRepository = new TipoPermisoRepository(_context);
            PermissionRepository = new PermissionRepository(_context);
        }

        public ITipoPermisoRepository TipoPermisoRepository { get; private set; }
        public IPermissionRepository PermissionRepository { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
