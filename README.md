# examen-net-fullstack

## Descripción

Proyecto desarrollado para evaluación técnica, que consta de un backend en ASP.NET CORE 8 y un frontend en Angular 19. 

La solución integra gestión de tiendas, clientes, artículos y compras, con una base de datos en SQL Server 2022.

---

## Estructura del Proyecto

Examen.NET/
│
├── Bussiness/ 		# Lógica de negocio .NET
├── Data/ 			# Acceso a datos .NET
├── Entitys/	    # Modelos y entidades .NET
├── Front/Examen-angular/ 		# Aplicación frontend Angular 19
└── SQLQuery3.sql 				# Script para creación de base de datos

---

## Base de Datos

- Motor: **SQL Server 2022**
- Tablas principales:
  - `clientes`
  - `tienda`
  - `articulos`
  - `articuloTienda` (relación muchos a muchos entre artículos y tiendas)
  - `clientesArticulo` (relación clientes-artículos con detalle de compras)

El script para creación de la base de datos está incluido en `SQLQuery3.sql`.

---

## Backend (.NET Core)

- Framework: .NET Core 8
- Entorno: Visual Studio
- URL y puerto de ejecución: `https://localhost:44362/`
- Para ejecutar:
  1. Abrir la solución en Visual Studio.
  2. Restaurar paquetes NuGet.
  3. Ejecutar el proyecto.
  4. Asegurarse que la API corra en `https://localhost:44362/`.

---

## Frontend (Angular)

- Framework: Angular 19
- Ubicación: `Examen.NET\Front\Examen-angular`
- Entorno: Visual Studio Code
- Para ejecutar:
  1. Abrir terminal en `Examen-angular`.
  2. Ejecutar `npm install`.
  3. Ejecutar `ng serve`.
  4. La app corre típicamente en `http://localhost:4200/`.
  5. **Importante:** El frontend está configurado para consumir la API en `https://localhost:44362/`
		     El bakend esta configurado para recibir las peticiones del frontend por `http://localhost:4200/`o`https://localhost:4200/`
		     Si la dirección es otra deberán cambiarla en el program.cs se encuentra en `Examen.NET\Program.cs`.
---

## Requisitos previos

- .NET 8 SDK instalado
- Node.js (última versión estable)
- Angular CLI instalado (`npm install -g @angular/cli`)
- SQL Server 2022

---

## Consideraciones

- Mantener puerto backend constante para evitar errores de conexión con frontend.
- Base de datos debe estar creada y configurada antes de ejecutar el backend.
- Se recomienda ejecutar backend y frontend en paralelo para correcto funcionamiento.

---

## Autor

Juan Carlos Canales Yonca

