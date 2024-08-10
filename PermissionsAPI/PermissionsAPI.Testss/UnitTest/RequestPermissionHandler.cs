using Moq;
using NSubstitute;
using PermissionsAPI.Data.Repositories;
using PermissionsAPI.Models;
using PermissionsAPI.Services.CQRS.Commands;
using PermissionsAPI.Services.CQRS.Handlers;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class RequestPermissionHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IPermissionRepository> _permissionRepositoryMock;
    private readonly RequestPermissionHandler _handler;

    public RequestPermissionHandlerTests()
    {
         _unitOfWorkMock = new Mock<IUnitOfWork>();
        _permissionRepositoryMock = new Mock<IPermissionRepository>();
        _unitOfWorkMock.Setup(x => x.PermissionRepository).Returns(_permissionRepositoryMock.Object);

        _handler = new RequestPermissionHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_GivenValidRequest_ShouldPersistPermission()
    {
        var command = new RequestPermissionCommand
        {
            NombreEmpleado = "Gustavo",
            ApellidoEmpleado = "Guerra",
            TipoPermisoId = 1,
            FechaPermiso = DateTime.Now
        };
        _permissionRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Permission>()))
            .Returns(Task.CompletedTask);

        _unitOfWorkMock.Setup(x => x.CompleteAsync())
            .ReturnsAsync(1);
        var result = await _handler.Handle(command, CancellationToken.None);
        _permissionRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Permission>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CompleteAsync(), Times.Once);

        Assert.NotNull(result);
        Assert.Equal("Gustavo", result.NombreEmpleado);
    }
}
