USE GastosDB;
GO

SELECT Id, Descripcion, Monto, Categoria
FROM Gastos
ORDER BY Id;