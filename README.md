<div align="center">

# 🏢 StaffCore RD

### Sistema de Gestión de Personal

**ISW-311 — Tecnologías de Internet I**
Universidad Central del Este

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-MVC-blue?style=for-the-badge&logo=dotnet)
![EF Core](https://img.shields.io/badge/Entity_Framework-Core-6DB33F?style=for-the-badge&logo=nuget)
![SQL Server](https://img.shields.io/badge/SQL_Server-LocalDB-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white)

</div>

---

## 👤 Datos del estudiante

| | |
|---|---|
| **Nombre** | Ramon Elías L. M. |
| **Matrícula** | RL2024-0453 |
| **Docente** | Iván Zorrilla Mateo |
| **Materia** | Tecnologías del Internet |
| **Asignación** | Proyecto Final |

---

## 📋 Descripción

**StaffCore RD** es un sistema web de gestión de personal para una empresa de servicios dominicana, con sede en Santo Domingo. Permite administrar empleados de cuatro departamentos —Tecnología, Recursos Humanos, Finanzas y Operaciones— con autenticación, control de acceso por roles y operaciones CRUD completas.

Construido con **ASP.NET Core MVC (.NET 8)**, **Entity Framework Core** (Code First) para la persistencia de datos, y **ASP.NET Identity** para el manejo de usuarios, contraseñas y roles.

---

## ✨ Funcionalidades

- 🔐 Registro e inicio de sesión con Identity, bloqueo por intentos fallidos
- 👥 CRUD completo de personal (crear, ver, editar, eliminar)
- 🛡️ Control de acceso por roles: **Administrador**, **RRHH**, **Viewer**
- 🔍 Buscador en tiempo real (sin recargar la página)
- 📄 Vista de detalle individual por empleado
- 📊 Resumen estadístico de nómina agrupado por departamento
- ⚙️ Panel de gestión de usuarios y roles (exclusivo Administrador)
- 💰 Cálculo automático del total de nómina

---

## 🧱 Tecnologías utilizadas

| Categoría | Tecnología |
|---|---|
| Framework | ASP.NET Core MVC (.NET 8) |
| Acceso a datos | Entity Framework Core (Code First) |
| Base de datos | SQL Server LocalDB |
| Autenticación | ASP.NET Identity |
| Frontend | Razor Views + Bootstrap 5 |

---

## 🚀 Cómo ejecutar el proyecto

```bash
# 1. Clonar el repositorio
git clone https://github.com/ramon4569/StaffCoreRD.git

# 2. Abrir StaffCoreRD.sln en Visual Studio 2022+
```

3. **Restaurar paquetes NuGet** — Visual Studio lo hace automáticamente al abrir la solución.

4. **Verificar el connection string** en `appsettings.json` (por defecto usa `(localdb)\mssqllocaldb`):
   ```json
   "ConnectionStrings": {
     "StaffCore": "Server=(localdb)\\mssqllocaldb;Database=StaffCoreDB;Trusted_Connection=True;"
   }
   ```

5. **Aplicar la migración**, en la Consola del Administrador de paquetes:
   ```powershell
   Update-Database
   ```

6. **Ejecutar** con `F5` o:
   ```bash
   dotnet run
   ```

> Al iniciar la aplicación por primera vez, se crean automáticamente los roles `Administrador`, `RRHH` y `Viewer`.

---

## 🔑 Credenciales de prueba

| Rol | Correo | Contraseña |
|---|---|---|
| 🛡️ **Administrador** | `eliaslorar@gmail.com` | `Elias2929` |
| 👔 **RRHH** | `rrhh@staffcore.com` | `Rrhh123` |
| 👁️ **Viewer** | `viewer@staffcore.com` | `Elias2929` |

---

## 🛡️ Roles del sistema

| Rol | Ver listado | Crear | Editar | Eliminar | Gestionar usuarios |
|---|:---:|:---:|:---:|:---:|:---:|
| **Administrador** | ✅ | ✅ | ✅ | ✅ | ✅ |
| **RRHH** | ✅ | ✅ | ✅ | ❌ | ❌ |
| **Viewer** | ✅ | ❌ | ❌ | ❌ | ❌ |

> ℹ️ El registro público (`/Account/Register`) solo asigna automáticamente **Administrador** (al primer usuario del sistema) o **Viewer** (a los demás), por seguridad. El rol **RRHH** debe asignarse manualmente por un Administrador desde el panel `/Usuarios`.

---

## 📁 Estructura del proyecto

```
StaffCoreRD/
├── Controllers/     → AccountController, StaffController, UsuariosController
├── Models/          → Staff, LoginViewModel, RegisterViewModel, ResumenDepartamentoViewModel
├── Data/            → StaffDbContext (IdentityDbContext)
├── Migrations/       → Migración inicial (IniciarStaffCore)
├── Views/           → Account/, Staff/, Usuarios/, Shared/
└── Program.cs       → Configuración de Identity, roles y middleware
```

---

<div align="center">

📎 **Repositorio:** [github.com/ramon4569/StaffCoreRD](https://github.com/ramon4569/StaffCoreRD)

</div>
