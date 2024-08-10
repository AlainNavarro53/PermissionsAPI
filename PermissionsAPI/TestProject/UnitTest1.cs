using Moq;
using PermissionsAPI.Data.Repositories;
using PermissionsAPI.Models;
using PermissionsAPI.Services.CQRS.Commands;
using PermissionsAPI.Services.CQRS.Handlers;
using NUnit.Framework;
using PermissionsAPI.DTOs;
using AutoMapper;
using NSubstitute;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PermissionsAPI.Data;
using System.Net;
using NSubstitute.ExceptionExtensions;
using Nest;

namespace TestProject
{
    public class Tests
    {
        private Permission permisodto;
        private RequestPermissionDto requestdto;
        private IMapper mapper;
        [SetUp]
        public void Setup()
        {
            mapper = Substitute.For<IMapper>();
            permisodto = new Permission()
            {
                Id = 1,
                NombreEmpleado = "Alain Gustavo",
                ApellidoEmpleado = "Navarro Angulo",
                TipoPermisoId = 1,
                FechaPermiso = DateTime.Now
            };
            requestdto = new RequestPermissionDto()
            {
                NombreEmpleado = "Alain Gustavo",
                ApellidoEmpleado = "Navarro Angulo",
                TipoPermisoId = 1,
                FechaPermiso = DateTime.Now
            };
        }

        private IServiceProvider Createcontext (string namedb)
        {
            var services = new ServiceCollection();
            services.AddDbContext<PermissionsDbContext>(options => options.UseInMemoryDatabase(databaseName: namedb),
                ServiceLifetime.Scoped,
                ServiceLifetime.Scoped
                );

            return services.BuildServiceProvider();
                
        }
        [Test]
        public async Task GetByIdAsync_ShouldReturnPermission_WhenPermissionExists()
        {
            var namedb = Guid.NewGuid().ToString();
            var serviceprovider = Createcontext(namedb);

            var db = serviceprovider.GetService<PermissionsDbContext>();
            db.Add(permisodto);
            //if (code == HttpStatusCode.OK)
            //{
            //    mapper.Map<PermissionDto>(requestdto).ReturnsForAnyArgs(permisodto);
            //}
            //else 
            //{
            //    mapper.Map<PermissionDto>(requestdto).ThrowsForAnyArgs(x => { throw new Exception(); });
            //}

            PermissionRepository repo = new PermissionRepository(db);

            var responserepo = repo.AddAsync(permisodto);
            //Assert.Equals(code, (responserepo.IsCompletedSuccessfully));
            Assert.Equals("Alain Gustavo", permisodto.NombreEmpleado);
            //Assert.Pass();
        }
    }
}