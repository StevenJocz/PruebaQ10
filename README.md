# 📚 Sistema de Inscripción de Materias

Este sistema permite gestionar estudiantes e inscribirlos a materias, asegurando que ningún estudiante se inscriba en más de 3 materias con más de 4 créditos cada una.

---

## 🚀 Tecnologías utilizadas

- ASP.NET Core Razor Pages (.NET 7)
- API REST con ASP.NET Core
- Bootstrap 5
- C#
- SQL Server

---

## ⚙️ Configuración del entorno

### 🔧 1. Configurar conexión a la base de datos (API)

Abre el archivo `appsettings.json` del proyecto **API** y actualiza la cadena de conexión en la sección `ConnectionStrings`:

```json
"AllowedHosts": "*",
"ConnectionStrings": {
  "DataBase": "Data Source=192.168.3.17;Initial Catalog=PruebaQ10;User ID=sa;Password=123456;TrustServerCertificate=True;MultipleActiveResultSets=True;Pooling=True;Max Pool Size=100;PoolBlockingPeriod=NeverBlock;"
},
```
- Tambien cambia donde apunta la interfaz
```json
"frontend": "http://localhost:5033"
```

### 🌐 2. Configurar la URL base de la API (Web)

Abre el archivo `appsettings.json` del proyecto **Interfaz** y modifica la URL base de la API:

```json
"ApiSettings": {
  "BaseUrl": "http://localhost:5001" // Reemplaza con la URL donde se ejecuta la API
}
```

---

## 🔄 Restaurar paquetes y ejecutar el proyecto

Desde la raíz del repositorio, ejecuta los siguientes comandos para compilar y ejecutar los proyectos:

```bash
dotnet restore
dotnet build
dotnet run
```

> Ejecuta primero la **API**, luego la **Interfaz (Frontend)**.

---

## 📂 Estructura del proyecto

```
/Interfaz         => Proyecto Razor Pages (Frontend)
/Api              => Proyecto ASP.NET Core API (Backend)
/Domain           => Proyecto con DTOs y entidades compartidas
/Persistence      => Proyecto con lógica de persistencia y acceso a datos
/Infrastructure   => Proyecto con servicios externos e infraestructura cruzada
/README.md        => Instrucciones del proyecto
```

---

## ✅ Funcionalidades principales

- Registro, edición y visualización de estudiantes y materias.
- Inscripción de materias por estudiante.

---

## 📌 Notas

- Asegúrate de tener SQL Server corriendo y accesible desde la IP configurada.
- Puedes usar herramientas como Postman para probar los endpoints de la API directamente.
