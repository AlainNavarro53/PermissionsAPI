using Microsoft.EntityFrameworkCore;
using PermissionsAPI.Data;
using PermissionsAPI.Data.Repositories;
using PermissionsAPI.Models;
using Xunit;

public class PermissionRepositoryTests
{
    private readonly PermissionRepository _repository;
    private readonly PermissionsDbContext _dbContext;

    public PermissionRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<PermissionsDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _dbContext = new PermissionsDbContext(options);
        _repository = new PermissionRepository(_dbContext);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnPermission_WhenPermissionExists()
    {
        var permission = new Permission { Id = 100, NombreEmpleado = "Prueba nombre", ApellidoEmpleado = "Prueba Apellido", TipoPermisoId = 1, FechaPermiso = DateTime.Now };
        _dbContext.Permissions.Add(permission);
        await _dbContext.SaveChangesAsync();

        var allPermissions = await _dbContext.Permissions.ToListAsync();
        Assert.Single(allPermissions);
        Assert.NotNull(allPermissions);
        Assert.Equal("Prueba nombre", allPermissions.FirstOrDefault().NombreEmpleado);
    }
}
