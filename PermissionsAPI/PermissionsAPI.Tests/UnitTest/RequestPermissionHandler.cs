using Moq;
using PermissionsAPI.Data.Repositories;
using PermissionsAPI.Models;
using PermissionsAPI.Services.CQRS.Commands;
using PermissionsAPI.Services.CQRS.Handlers;
using Xunit;

public class RequestPermissionHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly RequestPermissionHandler _handler;

    public RequestPermissionHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new RequestPermissionHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldPersistPermission()
    {
        var command = new RequestPermissionCommand
        {
            NombreEmpleado = "John",
            ApellidoEmpleado = "Doe",
            TipoPermisoId = 1,
            FechaPermiso = DateTime.Now
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        _unitOfWorkMock.Verify(x => x.PermissionRepository.AddAsync(It.IsAny<Permission>()), Times.Once);
        Assert.NotNull(result);
        Assert.Equal("John", result.NombreEmpleado);
    }
}
