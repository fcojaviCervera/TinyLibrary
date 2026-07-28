# TinyLibrary

Aplicación backend de una pequeña biblioteca aplicando Clean Architecture. El dominio es intencionadamente sencillo para que el foco esté en la arquitectura.

  ## Requisitos funcionales

  - Alta de libro (título, autor, ISBN único, nº de copias totales).
  - Alta de socio (nombre, email único, fecha de alta).
  - Prestar un libro a un socio, con estas reglas:
    - Un socio no puede tener más de 3 préstamos activos simultáneos.
    - No se puede prestar si no hay copias disponibles.
    - No se puede prestar a un socio con préstamos vencidos sin devolver.
    - Duración por defecto del préstamo: 14 días.
  - Devolver un libro. Si se devuelve con más de 7 días de retraso, se registra una penalización al socio (30 días sin poder pedir prestado). No hace falta modelar cobros ni multas monetarias.
  - Consultar los préstamos activos de un socio y el catálogo con copias disponibles.

  ## Requisitos técnicos (obligatorios)

  - Clean Architecture con al menos 4 proyectos: Domain, Application, Infrastructure, WebApi, con la dirección de dependencias correcta.
  - Las reglas de negocio deben vivir en el dominio, no en controladores ni en servicios de aplicación. Se debe poder testearlas sin instanciar EF ni la API.
  - Persistencia definida mediante interfaces en Application/Domain e implementada en Infrastructure con EF Core sobre SQLite (fichero o en memoria).
  - API REST con al menos: `POST /libros`, `POST /socios`, `POST /prestamos`, `POST /prestamos/{id}/devolver`, `GET /socios/{id}/prestamos`, `GET /libros`.
  - Validación de entrada (FluentValidation o DataAnnotations) y manejo global de errores con códigos HTTP correctos (400/404/409).
  - Tests unitarios del dominio: al menos uno por cada una de las 4 reglas del préstamo.
  - Test de integración de un flujo completo (prestar → devolver con retraso → nuevo préstamo denegado).
  - README breve con: cómo ejecutar y decisiones de diseño.

  ## Lo que NO hace falta

  Autenticación, frontend, Docker, CQRS ni MediatR.

  ## Stack

  .NET 10, C#, EF Core, NUnit, ASP.NET Core con Controllers.

  ## Cómo ejecutar

  ```bash
  dotnet run --project src/TinyLibrary.WebApi
  dotnet test
  ```

  Con el perfil de lanzamiento por defecto (http), la API queda disponible en http://localhost:5226, y la interfaz interactiva en http://localhost:5226/scalar/v1.

  Arquitectura

  WebApi -> Infrastructure -> Application -> Domain

  Decisiones tomadas durante el diseño

  - Se decidió añadir las restricciones de negocio en la capa de Domain para que se puedan testear sin depender de Infrastructure.
  - TimeProvider inyectado. Hace que las reglas de dominio sean deterministas y testeables (el test de integración simula el paso del tiempo con FakeTimeProvider.Advance, sin esperas reales).
  - Excepciones tipadas en lugar de Result<T>. Simplifica el código y es más legible para un proyecto de alcance tan corto.
  - NotFoundException/DuplicateIsbnException/DuplicateEmailMemberException viven en Application y no en Domain, ya que son problemas de la persistencia, no invariantes de la entidad.
  - Unicidad de ISBN/email con doble comprobación: chequeo en el caso de uso además del índice único en BD, para cubrir condiciones de carrera.
  - Repository + IUnitOfWork para evitar guardados parciales y obtener resultados atómicos.
  - DbContextOptions<LibraryDbContext> genérico en vez de la versión no genérica: evita ambigüedades si en el futuro se registra más de un DbContext en el mismo contenedor DI.
  - IExceptionHandler + ProblemDetails para el middleware global de errores.
  - FluentValidation enganchado con un filtro global (IAsyncActionFilter) en vez de validación explícita repetida en cada controlador.
  - Test de integración con SQLite en memoria compartida + FakeTimeProvider, sustituyendo los registros reales del contenedor DI en WebApplicationFactory.ConfigureWebHost.

  Qué queda fuera

  - Test de integración extra de ISBN duplicado y socio inexistente.
  - Sin autenticación / autorización.
  - Sin paginación en GET /libros.