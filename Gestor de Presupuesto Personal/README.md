# Balancea — Gestor de Presupuesto Personal

> **Sistema Distribuido de Gestion de Finanzas Personales** desarrollado bajo principios de **Arquitectura en Capas**, **Programacion Orientada a Objetos (POO)**, backend en **ASP.NET Core Web API (.NET 9)** con **Entity Framework Core**, y frontend moderno en **React**.

<p align="center">
  <img src="https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB" />
  <img src="https://img.shields.io/badge/Vite-646CFF?style=for-the-badge&logo=vite&logoColor=white" />
  <img src="https://img.shields.io/badge/Entity_Framework_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
  <img src="https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" />
</p>

<p align="center">
  <b>Balancea</b> — Tu dinero, bajo control
</p>

---

## Tabla de Contenidos

- [Descripcion](#descripcion)
- [Caracteristicas principales](#caracteristicas-principales)
- [Tecnologias utilizadas](#tecnologias-utilizadas)
- [Arquitectura del proyecto](#arquitectura-del-proyecto)
- [Modelo de datos](#modelo-de-datos)
- [Conceptos de POO aplicados](#conceptos-de-poo-aplicados)
- [Instalacion y ejecucion](#instalacion-y-ejecucion)
- [Estructura de carpetas](#estructura-de-carpetas)
- [Autor](#autor)

---

## Descripcion

**Balancea** resuelve el problema de llevar el control de las finanzas personales de forma manual. Es un sistema que centraliza el registro de **ingresos**, **gastos** y **categorias** en una interfaz web sencilla, calculando el **balance en tiempo real** de cada usuario.

El proyecto sigue una **arquitectura distribuida**: el frontend (React) y el backend (.NET) son aplicaciones independientes que se comunican unicamente a traves de una API REST.

## Caracteristicas principales

- Registro de **Usuarios**, **Categorias**, **Gastos** e **Ingresos** desde el frontend
- **Balance en tiempo real**, calculado en el backend y mostrado con una grafica
- Eliminacion de registros con confirmacion
- Seleccion de usuario para ver su balance individual
- **Tema claro y oscuro**
- Interfaz responsiva pensada para escritorio

## Tecnologias utilizadas

**Backend**
- ASP.NET Core Web API (.NET 9)
- Entity Framework Core
- SQL Server
- Swagger / OpenAPI

**Frontend**
- React + Vite
- Recharts (graficas)
- CSS con variables personalizadas (tema claro/oscuro)

## Arquitectura del proyecto

El backend esta organizado en 4 capas:

| Capa | Proyecto | Responsabilidad |
|---|---|---|
| **API** | `Gestor de Presupuesto Personal.API` | Expone los controllers REST, configura CORS y Swagger |
| **Application** | `PP.Application` | Contiene los servicios con la logica de negocio y las validaciones |
| **Infrastructure** | `PP.Infrastructure` | Repositorios, `DbContext` y conexion a SQL Server via EF Core |
| **Domain** | `PP.Domain` | Entidades del dominio e interfaces de repositorio |

El frontend (`balancea-web`) es un proyecto React independiente que consume la API mediante `fetch`.

```
React (balancea-web)  -->  API (.NET)  -->  Application  -->  Infrastructure  -->  SQL Server
```

## Modelo de datos

Todas las entidades heredan de una clase abstracta `BaseEntity`, que define atributos comunes y un metodo abstracto implementado de forma distinta en cada clase hija.

```
BaseEntity (abstracta)
 ├── Usuario     (Nombre, Correo)
 ├── Categoria   (Nombre, Tipo)
 ├── Gasto       (Monto, Fecha, UsuarioId, CategoriaId)
 └── Ingreso     (Monto, Fecha, UsuarioId, CategoriaId)
```

## Conceptos de POO aplicados

| Concepto | Donde se aplica |
|---|---|
| **Clases abstractas** | `BaseEntity` define el metodo abstracto `ObtenerDescripcion()` |
| **Sobrecarga de metodos** | `UsuarioService.Validar()` tiene una version que recibe un `Usuario` y otra que recibe `nombre` y `correo` por separado |
| **Constructores** | Cada entidad inicializa sus atributos al crearse |
| **Herencia** | Las 4 entidades del dominio heredan de `BaseEntity` |

## Instalacion y ejecucion

### Requisitos previos
- .NET 9 SDK
- Node.js (con npm)
- SQL Server

### Backend

```bash
cd "Gestor de Presupuesto Personal.API"
dotnet restore
dotnet run
```

La API queda disponible en `https://localhost:7127`, con Swagger en `https://localhost:7127/swagger`.

### Frontend

```bash
cd balancea-web
npm install
npm run dev
```

El frontend queda disponible en `http://localhost:5173`.

> Ambos procesos (backend y frontend) deben estar corriendo al mismo tiempo para que la aplicacion funcione completa.

## Estructura de carpetas

```
Gestor de Presupuesto Personal/
├── Gestor de Presupuesto Personal.API/   # Controllers, Program.cs, Swagger
├── PP.Application/                       # Servicios y logica de negocio
├── PP.Infrastructure/                    # Repositorios y DbContext
├── PP.Domain/                             # Entidades e interfaces
└── balancea-web/                         # Frontend en React
```

## Autor

**Stevens David Ricardo Mendez**
Matricula: 20250059