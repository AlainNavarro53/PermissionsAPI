PermissionsAPI

Descripción
PermissionsAPI es una aplicación ASP.NET Core construida para gestionar permisos de empleados y tipos de permisos. La aplicación está diseñada utilizando la arquitectura de capas, con el backend desarrollado en .NET 6 y el frontend construido en React con Material-UI. La aplicación incluye una base de datos SQL Server para almacenar datos.

Estructura del Proyecto

Backend - PermissionsAPI

Este proyecto está construido en .NET 6 y utiliza varias bibliotecas y paquetes, incluyendo AutoMapper, MediatR, Entity Framework Core, y Elasticsearch. 

También soporta pruebas unitarias e integradas.


Características principales:


API REST: Permite la gestión de permisos y tipos de permisos.

CQRS: La aplicación sigue el patrón CQRS (Command Query Responsibility Segregation) utilizando MediatR.

Elasticsearch y Kafka: Integración con Elasticsearch para búsquedas avanzadas y Kafka para la mensajería.

Pruebas: Soporta pruebas unitarias e integradas utilizando MSTest y Xunit.

Docker: Incluye configuraciones de Docker para el despliegue en entornos Linux.


Dependencias principales:

.NET 6

AutoMapper

MediatR

Entity Framework Core (SQL Server)

Moq, NSubstitute (para mocking en pruebas)

MSTest y Xunit (para pruebas)

Elasticsearch y Kafka


Estructura de Carpetas:

Controllers/: Controladores de la API

Services/: Servicios y handlers para CQRS

CQRS/

Commands/

Handlers/

Queries/

Data/

Repositories/

DTOs/: Objetos de Transferencia de Datos

Migrations/: Migraciones de la base de datos


Backend - PermissionsAPI.Testss

Este proyecto contiene pruebas unitarias e integradas para el proyecto PermissionsAPI. 

Se han utilizado Xunit y MSTest para las pruebas.


Características:

Pruebas unitarias: Aseguran que los componentes individuales de la aplicación funcionen según lo esperado.

Pruebas de integración: Verifican que los componentes del sistema trabajen juntos correctamente.


Dependencias principales:

Xunit

Moq

MSTest


Estructura de Carpetas:

UnitTest/: Contiene pruebas unitarias para los diferentes componentes.

PermissionRepositoryTests.cs

RequestPermissionHandlerTests.cs

IntegrationTests/: Contiene pruebas de integración.

PermissionsApiIntegrationTests.cs


Frontend

El frontend está construido en React con Material-UI para el diseño y la interacción.


Características:


Componentes React: Manejan la interfaz de usuario y la lógica de interacción.

Material-UI: Proporciona componentes visuales interactivos y estilizados.

Paginación: Implementada para la lista de permisos y tipos de permisos.

Navegación: React Router DOM para la navegación entre páginas.

Interacción con API: Axios se utiliza para interactuar con la API backend.


Estructura de Carpetas:

src/

components/: Contiene los componentes principales de React.

RequestPermissionForm.js

PermissionList.js

TipoPermisoList.js

CreateTipoPermisoForm.js

Home.js

endpoints.ts: Archivo para gestionar las URLs de la API.

public/

index.html: Archivo HTML principal.

favicon.ico: Ícono del sitio.

Dockerfile: Archivo para construir el contenedor Docker del frontend.

docker-compose.yml: Archivo para orquestar los contenedores Docker.

Base de Datos

La base de datos utilizada es SQL Server. 

El proyecto incluye migraciones de Entity Framework Core para gestionar la estructura de la base de datos.


Tablas principales:

Permissions: Almacena los permisos de los empleados.

TipoPermisos: Almacena los diferentes tipos de permisos.

Un backup de la base de datos está adjunto en la documentación para su restauración.
