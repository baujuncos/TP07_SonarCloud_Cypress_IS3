# TikTask2.0 - Guía Rápida

## Descripción

TikTask2.0 es una aplicación web completa de gestión de tareas construida con .NET 9, que incluye:
- **Backend API** en ASP.NET Core Web API
- **Frontend** en Blazor Server
- **Base de datos** SQLite
- **Autenticación** con JWT
- **Dos tipos de usuarios**: Usuario normal y Administrador

## Características Principales

### Usuario Normal
- ✅ Registrarse e iniciar sesión
- ✅ Crear tareas con título, descripción y fecha de vencimiento
- ✅ Editar sus propias tareas
- ✅ Marcar tareas como completadas
- ✅ Eliminar tareas
- ✅ Ver solo sus propias tareas

### Usuario Administrador
- ✅ Todo lo que puede hacer un usuario normal
- ✅ Ver las tareas de todos los usuarios
- ✅ Ver el nombre de usuario asociado a cada tarea
- ✅ No puede editar ni eliminar tareas de otros usuarios

## Inicio Rápido

### Requisitos Previos
- .NET 9.0 SDK instalado
- Un editor de código (Visual Studio, VS Code, etc.)

### Instalación y Ejecución

1. **Clonar el repositorio**
```bash
git clone https://github.com/baujuncos/TP5_IS3.git
cd TP5_IS3
```

2. **Ejecutar la API (Terminal 1)**
```bash
cd src/TikTask2.0.API
dotnet run --launch-profile http
```

La API se ejecutará en: http://localhost:5001

3. **Ejecutar la Aplicación Web (Terminal 2)**
```bash
cd src/TikTask2.0.Web
dotnet run --launch-profile http
```

La aplicación web se ejecutará en: http://localhost:5002

4. **Abrir el navegador**

Navega a: http://localhost:5002

## Credenciales por Defecto

### Usuario Administrador
- **Usuario**: `admin`
- **Contraseña**: `Admin123!`

### Crear Usuario Normal
1. Haz clic en "Register here" en la página de login
2. Completa el formulario con tus datos
3. Haz clic en "Register"

## Uso de la Aplicación

### 1. Registro/Login
- Al abrir la aplicación, serás redirigido a la página de login
- Puedes registrarte o iniciar sesión con el usuario admin

### 2. Crear una Tarea
1. Haz clic en el botón "Add Task"
2. Completa el formulario:
   - **Title**: Título de la tarea
   - **Description**: Descripción detallada
   - **Due Date**: Fecha de vencimiento
3. Haz clic en "Save"

### 3. Gestionar Tareas
- **Completar**: Haz clic en "Complete" para marcar una tarea como completada
- **Editar**: Haz clic en "Edit" para modificar una tarea
- **Eliminar**: Haz clic en "Delete" para eliminar una tarea

### 4. Funcionalidad de Administrador
1. Inicia sesión como administrador
2. Verás un botón "All Tasks" en la parte superior
3. Haz clic para ver las tareas de todos los usuarios
4. Haz clic en "My Tasks" para volver a tus tareas

## Estructura del Proyecto

```
TikTask2.0/
├── src/
│   ├── TikTask2.0.API/          # Backend API
│   │   ├── Controllers/         # Controladores de API
│   │   ├── Data/               # Contexto de base de datos
│   │   ├── Models/             # Modelos de datos
│   │   ├── Services/           # Servicios de negocio
│   │   └── DTOs/               # Objetos de transferencia de datos
│   └── TikTask2.0.Web/          # Frontend Blazor
│       ├── Components/          # Componentes Blazor
│       ├── Models/             # Modelos del cliente
│       └── Services/           # Servicios de cliente
├── DEPLOYMENT.md               # Guía de despliegue
├── TESTING.md                  # Guía de testing
└── README.md                   # Documentación general
```

## Tecnologías Utilizadas

- **.NET 9.0**: Framework principal
- **ASP.NET Core Web API**: Backend RESTful
- **Blazor Server**: Frontend interactivo
- **Entity Framework Core**: ORM
- **SQLite**: Base de datos ligera
- **JWT**: Autenticación segura
- **BCrypt**: Encriptación de contraseñas
- **Bootstrap**: Estilos CSS

## Características de Seguridad

✅ Contraseñas encriptadas con BCrypt
✅ Autenticación JWT con expiración
✅ Autorización basada en roles
✅ Validación de datos en backend
✅ Protección contra acceso no autorizado

## Despliegue en Azure DevOps

El proyecto incluye un archivo `azure-pipelines.yml` configurado para:
1. Construcción automática del código
2. Publicación de artefactos
3. Despliegue automático a Azure App Services

Ver [DEPLOYMENT.md](DEPLOYMENT.md) para instrucciones detalladas.

## Despliegue con Docker

El proyecto incluye soporte completo para Docker:

```bash
# Construir y ejecutar con Docker Compose
docker-compose up -d

# Acceder a la aplicación
# API: http://localhost:5001
# Web: http://localhost:5002
```

Ver [DEPLOYMENT.md](DEPLOYMENT.md) para más opciones.

## Base de Datos

La aplicación utiliza SQLite, que crea automáticamente un archivo de base de datos en:
- `src/TikTask2.0.API/tiktask.db`

### Reiniciar la Base de Datos

```bash
cd src/TikTask2.0.API
rm tiktask.db
dotnet run
```

Esto recreará la base de datos y el usuario administrador por defecto.

## Pruebas

Ver [TESTING.md](TESTING.md) para guía completa de testing, incluyendo:
- Pruebas manuales paso a paso
- Pruebas de API con curl
- Verificación de base de datos
- Casos de prueba negativos

## Problemas Comunes

### La API no inicia
- Verifica que el puerto 5001 esté disponible
- Asegúrate de tener .NET 9.0 SDK instalado

### El Web no puede conectarse a la API
- Verifica que la API esté ejecutándose
- Revisa la configuración de `ApiUrl` en `appsettings.Development.json`

### Error de base de datos
- Elimina el archivo `tiktask.db` y reinicia la API
- Verifica permisos de escritura en la carpeta

## Soporte

Para reportar problemas o solicitar nuevas características:
- Abre un issue en: https://github.com/baujuncos/TP5_IS3/issues

## Licencia

Este proyecto está licenciado bajo la Licencia MIT.

## Autor

Desarrollado como parte del TP5 de Ingeniería de Software 3.

---

## Funcionalidades Implementadas ✅

Según los requisitos originales:

- [x] **Backend con .NET**: API RESTful completa en ASP.NET Core
- [x] **Frontend**: Aplicación Blazor Server interactiva
- [x] **Base de datos**: SQLite con Entity Framework Core
- [x] **Autenticación**: Sistema completo de registro e inicio de sesión
- [x] **Gestión de tareas**: CRUD completo con título, descripción y fecha de vencimiento
- [x] **Marcar como completadas**: Funcionalidad toggle para completar/descompletar
- [x] **Eliminar tareas**: Capacidad de eliminar tareas
- [x] **Usuario Administrador**: Puede ver tareas de todos los usuarios
- [x] **Azure DevOps ready**: Configuración completa para CI/CD
- [x] **Integración en la nube**: Preparado para despliegue en Azure

## Próximos Pasos

1. **Personalización**: Modifica la clave JWT en `appsettings.json` para producción
2. **Despliegue**: Sigue la guía en DEPLOYMENT.md para publicar en Azure
3. **Testing**: Ejecuta las pruebas descritas en TESTING.md
4. **Mejoras**: Considera agregar:
   - Notificaciones por email
   - Categorías de tareas
   - Prioridades
   - Adjuntos de archivos
   - Búsqueda y filtros avanzados
