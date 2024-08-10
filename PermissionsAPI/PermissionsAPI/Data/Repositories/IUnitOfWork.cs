namespace PermissionsAPI.Data.Repositories
{
    public interface IUnitOfWork
    {
        IPermissionRepository PermissionRepository { get; }

        ITipoPermisoRepository TipoPermisoRepository { get; }
        Task<int> CompleteAsync();
    }
}
