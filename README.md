# MS-Catalog - Microservicio de Catálogos

## Resumen del Proyecto

Este proyecto implementa un microservicio de catálogos con **arquitectura hexagonal** desarrollado en **.NET 8** con **Entity Framework Core** y **PostgreSQL**.

## Arquitectura Implementada

### Estructura del Proyecto
```
ms-catalog/
├── Domain/
│   ├── Models/           # Entidades de dominio con DataAnnotations snake_case
│   └── Ports/
│       ├── Inbound/      # Interfaces de casos de uso
│       └── Outbound/     # Interfaces de repositorios
├── Application/
│   └── Services/         # Implementación de casos de uso
└── Infrastructure/
    ├── Controllers/      # API Controllers
    ├── Repositories/     # Implementación de repositorios
    ├── Data/            # DbContext
    └── Extensions/      # Configuración DI
```

## Base de Datos

- **Servidor**: 66.94.126.223:5432
- **Base de datos**: neocore-dev
- **Schema**: catalog
- **Usuario**: neocore

### Tablas Creadas
1. **identification_types** - Tipos de identificación
2. **economic_activities** - Actividades económicas  
3. **genders** - Géneros
4. **countries** - Países
5. **provinces** - Provincias (FK: country_id)
6. **cities** - Ciudades (FK: province_id)

## Endpoints Disponibles

### Catálogos Independientes (CRUD completo)

#### 1. Identification Types
```
GET    /ms-catalog/identification-types
POST   /ms-catalog/identification-types
GET    /ms-catalog/identification-types/{id}
PUT    /ms-catalog/identification-types/{id}
DELETE /ms-catalog/identification-types/{id}
```

#### 2. Economic Activities
```
GET    /ms-catalog/economic-activities
POST   /ms-catalog/economic-activities
GET    /ms-catalog/economic-activities/{id}
PUT    /ms-catalog/economic-activities/{id}
DELETE /ms-catalog/economic-activities/{id}
```

#### 3. Gender
```
GET    /ms-catalog/genders
POST   /ms-catalog/genders
GET    /ms-catalog/genders/{id}
PUT    /ms-catalog/genders/{id}
DELETE /ms-catalog/genders/{id}
```

### Catálogos Jerárquicos (Ubicación Geográfica)

#### 1. Countries
```
GET    /ms-catalog/countries
POST   /ms-catalog/countries
GET    /ms-catalog/countries/{id}
PUT    /ms-catalog/countries/{id}
DELETE /ms-catalog/countries/{id}
GET    /ms-catalog/countries/{countryId}/provinces
```

#### 2. Provinces
```
GET    /ms-catalog/provinces
POST   /ms-catalog/provinces
GET    /ms-catalog/provinces/{id}
PUT    /ms-catalog/provinces/{id}
DELETE /ms-catalog/provinces/{id}
GET    /ms-catalog/provinces?countryId={countryId}
GET    /ms-catalog/provinces/{provinceId}/cities
```

#### 3. Cities
```
GET    /ms-catalog/cities
POST   /ms-catalog/cities
GET    /ms-catalog/cities/{id}
PUT    /ms-catalog/cities/{id}
DELETE /ms-catalog/cities/{id}
GET    /ms-catalog/cities?provinceId={provinceId}
GET    /ms-catalog/cities?countryId={countryId}
```

## Tecnologías Utilizadas

- **.NET 8**
- **ASP.NET Core Web API**
- **Entity Framework Core 8.0**
- **PostgreSQL** (Npgsql.EntityFrameworkCore.PostgreSQL)
- **Swagger/OpenAPI**
- **Arquitectura Hexagonal (Puertos y Adaptadores)**

## Configuración

### Connection String
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=66.94.126.223;Port=5432;Database=neocore-dev;Username=neocore;Password=K7qlv33274HGCw1mMz"
  }
}
```

### Características Implementadas
- **CORS** configurado para desarrollo
- **JSON serialization** con snake_case para BD
- **Soft delete** (campo status boolean)
- **Timestamps** (created_at, updated_at)
- **Relaciones jerárquicas** con Foreign Keys
- **Índices automáticos** en claves foráneas

## Comandos de Desarrollo

### Para nuevos desarrolladores:
```bash
git pull
dotnet restore
dotnet ef database update
dotnet run
```

### Para crear nuevas migraciones:
```bash
dotnet ef migrations add <NombreMigration> --context CatalogDbContext
dotnet ef database update --context CatalogDbContext
```

### Ejecutar la aplicación:
```bash
dotnet run
```

## URLs de Desarrollo
- **API**: http://localhost:5014
- **Swagger UI**: http://localhost:5014/swagger

## Estado del Proyecto

✅ **COMPLETADO** - Todas las funcionalidades implementadas:

1. ✅ Domain layer con modelos y DataAnnotations snake_case
2. ✅ Application layer con servicios de casos de uso  
3. ✅ Infrastructure layer con controllers y repositorios
4. ✅ DbContext configurado para schema catalog
5. ✅ Migraciones EF Core aplicadas exitosamente
6. ✅ Inyección de dependencias configurada
7. ✅ Program.cs actualizado con arquitectura hexagonal
8. ✅ NuGet packages instalados (EF Core, PostgreSQL)
9. ✅ Base de datos conectada y funcionando
10. ✅ Aplicación ejecutándose correctamente

## Próximos Pasos Sugeridos

- [ ] Agregar validaciones de negocio en el dominio
- [ ] Implementar logging estructurado
- [ ] Agregar tests unitarios y de integración
- [ ] Implementar cache con Redis
- [ ] Agregar autenticación/autorización
- [ ] Implementar paginación en listados
- [ ] Documentar APIs con ejemplos
- [ ] Configurar health checks
- [ ] Implementar métricas y monitoreo

---

**Nota**: Este archivo fue generado automáticamente. Cárgalo en la próxima sesión para continuar el desarrollo del proyecto.