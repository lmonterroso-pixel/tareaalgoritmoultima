using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

public class Program
{
    public static void Main()
    {
        GastoRepositorio repositorio = new GastoRepositorio();
        string opcion = "";

        do
        {
            opcion = LeerOpcionMenu();

            switch (opcion)
            {
                case "1":
                    RegistrarGasto(repositorio);
                    break;

                case "2":
                    ListarGastos(repositorio);
                    break;

                case "3":
                    BuscarGasto(repositorio);
                    break;

                case "4":
                    ActualizarMonto(repositorio);
                    break;

                case "5":
                    EliminarGasto(repositorio);
                    break;

                case "6":
                    Console.WriteLine("Saliendo...");
                    break;
                    
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
        while (opcion != "6");
    }

    private static string LeerOpcionMenu()
    {
        Console.WriteLine("\n=== REGISTRO DE GASTOS ===");
        Console.WriteLine("1. Registrar gasto");
        Console.WriteLine("2. Listar gastos");
        Console.WriteLine("3. Buscar gasto por Id");
        Console.WriteLine("4. Actualizar monto");
        Console.WriteLine("5. Eliminar gasto");
        Console.WriteLine("6. Salir");
        Console.Write("Elige una opción: ");

        return Console.ReadLine();
    }

    private static void RegistrarGasto(
        GastoRepositorio repositorio)
    {
        Console.Write("Descripción: ");
        string descripcion = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(descripcion))
        {
            Console.WriteLine("La descripción no puede estar vacía.");
            return;
        }

        Console.Write("Monto: ");

        if (!decimal.TryParse(
                Console.ReadLine(), out decimal monto)
            || monto <= 0)
        {
            Console.WriteLine("Monto inválido.");
            return;
        }

        Console.Write("Categoría: ");
        string categoria = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(categoria))
        {
            Console.WriteLine("La categoría no puede estar vacía.");
            return;
        }

        Gasto gasto = new Gasto
        {
            Descripcion = descripcion.Trim(),
            Monto = monto,
            Categoria = categoria.Trim()
        };

        try
        {
            repositorio.Registrar(gasto);
            Console.WriteLine("Gasto registrado.");
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error al registrar: " + ex.Message);
        }
    }

    private static void ListarGastos(
        GastoRepositorio repositorio)
    {
        try
        {
            List<Gasto> gastos = repositorio.ListarTodos();

            if (gastos.Count == 0)
            {
                Console.WriteLine("No hay gastos registrados.");
                return;
            }

            decimal totalGastado = 0m;

            Console.WriteLine("\n=== GASTOS REGISTRADOS ===");

            for (int i = 0; i < gastos.Count; i++)
            {
                Console.WriteLine(gastos[i].ToString());
                totalGastado = totalGastado + gastos[i].Monto;
            }

            Console.WriteLine(
                $"TOTAL GASTADO: Q {totalGastado:N2}");
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error al listar: " + ex.Message);
        }
    }

    private static void BuscarGasto(
        GastoRepositorio repositorio)
    {
        Console.Write("Ingrese el Id del gasto: ");

        if (!int.TryParse(Console.ReadLine(), out int id)
            || id <= 0)
        {
            Console.WriteLine("Id inválido.");
            return;
        }

        try
        {
            Gasto? gasto = repositorio.BuscarPorId(id);

            if (gasto == null)
            {
                Console.WriteLine("No existe ese Id.");
            }
            else
            {
                Console.WriteLine(gasto.ToString());
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error al buscar: " + ex.Message);
        }
    }

    private static void ActualizarMonto(
        GastoRepositorio repositorio)
    {
        Console.Write("Ingrese el Id del gasto: ");

        if (!int.TryParse(Console.ReadLine(), out int id)
            || id <= 0)
        {
            Console.WriteLine("Id inválido.");
            return;
        }

        try
        {
            Gasto? gasto = repositorio.BuscarPorId(id);

            if (gasto == null)
            {
                Console.WriteLine("No existe ese Id.");
                return;
            }

            Console.WriteLine("Gasto actual:");
            Console.WriteLine(gasto.ToString());

            Console.Write("Ingrese el nuevo monto: ");

            if (!decimal.TryParse(
                    Console.ReadLine(), out decimal nuevoMonto)
                || nuevoMonto <= 0)
            {
                Console.WriteLine("Monto inválido.");
                return;
            }

            gasto.Monto = nuevoMonto;

            bool actualizado =
                repositorio.Actualizar(gasto);

            if (actualizado)
            {
                Console.WriteLine("Gasto actualizado.");
            }
            else
            {
                Console.WriteLine("No existe ese Id.");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error al actualizar: " + ex.Message);
        }
    }

    private static void EliminarGasto(
        GastoRepositorio repositorio)
    {
        Console.Write("Ingrese el Id del gasto: ");

        if (!int.TryParse(Console.ReadLine(), out int id)
            || id <= 0)
        {
            Console.WriteLine("Id inválido.");
            return;
        }

        try
        {
            bool eliminado = repositorio.Eliminar(id);

            if (eliminado)
            {
                Console.WriteLine("Gasto eliminado.");
            }
            else
            {
                Console.WriteLine("No existe ese Id.");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Error al eliminar: " + ex.Message);
        }
    }
}