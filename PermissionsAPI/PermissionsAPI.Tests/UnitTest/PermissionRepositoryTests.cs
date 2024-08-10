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
        var permission = new Permission { Id = 1, NombreEmpleado = "John", ApellidoEmpleado = "Doe" };
        _dbContext.Permissions.Add(permission);
        await _dbContext.SaveChangesAsync();

        // Verifica que la entidad se guardó
        var allPermissions = await _dbContext.Permissions.ToListAsync();
        Assert.Single(allPermissions);  // Debería haber una entidad en la base de datos

        var result = await _repository.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("John", result.NombreEmpleado);
    }
}
