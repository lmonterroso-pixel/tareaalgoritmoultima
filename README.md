# Tarea 11: Registro de Gastos

Este proyecto es un programa de consola en C# para administrar gastos. Permite registrar y listar gastos, buscar uno por su Id, actualizar su monto y eliminarlo. La información se guarda en SQL Server LocalDB.

## Requisitos

- Visual Studio Community 2026.
- .NET 8.
- SQL Server LocalDB.
- Paquete Microsoft.Data.SqlClient.

## Cómo preparar y ejecutar el proyecto

1. Abrir el proyecto CrudGastos en Visual Studio.
2. Ejecutar en SQL Server el script de la Clase 10 para crear o preparar GastosDB y la tabla Gastos.
3. Comprobar que la tabla tenga los tres gastos iniciales con Id del 1 al 3:
   - Almuerzo, Q 45.50, Comida.
   - Gasolina, Q 250.00, Transporte.
   - Cine, Q 75.00, Entretenimiento.
4. Revisar en GastoRepositorio.cs que la cadena de conexión use la instancia  (localdb)\GastosTarea y la base GastosDB.
5. En Visual Studio, ejecutar con **F5** o **Ctrl + F5**.

## Opciones del menú

1. Registrar gasto.
2. Listar gastos.
3. Buscar gasto por Id.
4. Actualizar monto.
5. Eliminar gasto.
6. Salir.


En el video explico , el repositorio, las consultas parametrizadas, la búsqueda por Id, la actualización, la eliminación, las validaciones y el manejo de errores. También ejecuto los escenarios.

Enlace público del video:https://drive.google.com/file/d/1eBkWVhU55If_-O25ly1ZfkCKqpNxYw3t/view?usp=sharing

## Archivos del programa

- Program.cs: menú, validaciones y comunicación con el usuario.
- Gasto.cs: clase que representa cada gasto.
- GastoRepositorio.cs: conexión y consultas a SQL Server.
- evidencias: capturas de los cinco escenarios.
- script_clase10.sql: script usado para preparar la base de datos, si se incluye en el repositorio.
