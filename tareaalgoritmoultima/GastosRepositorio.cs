using System.Collections.Generic;
using Microsoft.Data.SqlClient;

public class GastoRepositorio
{
    private const string CadenaConexion =
        @"Server=(localdb)\GastosTarea;" +
        @"Database=GastosDB;" +
        @"Trusted_Connection=True;" +
        @"TrustServerCertificate=True;";

    public void Registrar(Gasto gasto)
    {
        using (SqlConnection conexion =
            new SqlConnection(CadenaConexion))
        {
            conexion.Open();

            string sql =
                "INSERT INTO Gastos (Descripcion, Monto, Categoria) " +
                "VALUES (@Descripcion, @Monto, @Categoria)";

            using (SqlCommand comando =
                new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue(
                    "@Descripcion", gasto.Descripcion);

                comando.Parameters.AddWithValue(
                    "@Monto", gasto.Monto);

                comando.Parameters.AddWithValue(
                    "@Categoria", gasto.Categoria);

                comando.ExecuteNonQuery();
            }
        }
    }

    public List<Gasto> ListarTodos()
    {
        List<Gasto> gastos = new List<Gasto>();

        using (SqlConnection conexion =
            new SqlConnection(CadenaConexion))
        {
            conexion.Open();

            string sql =
                "SELECT Id, Descripcion, Monto, Categoria " +
                "FROM Gastos ORDER BY Id";

            using (SqlCommand comando =
                new SqlCommand(sql, conexion))
            using (SqlDataReader lector =
                comando.ExecuteReader())
            {
                while (lector.Read())
                {
                    Gasto gasto = new Gasto
                    {
                        Id = lector.GetInt32(0),
                        Descripcion = lector.GetString(1),
                        Monto = lector.GetDecimal(2),
                        Categoria = lector.GetString(3)
                    };

                    gastos.Add(gasto);
                }
            }
        }

        return gastos;
    }

    public Gasto? BuscarPorId(int id)
    {
        using (SqlConnection conexion =
            new SqlConnection(CadenaConexion))
        {
            conexion.Open();

            string sql =
                "SELECT Id, Descripcion, Monto, Categoria " +
                "FROM Gastos WHERE Id = @Id";

            using (SqlCommand comando =
                new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@Id", id);

                using (SqlDataReader lector =
                    comando.ExecuteReader())
                {
                    if (lector.Read())
                    {
                        Gasto gasto = new Gasto
                        {
                            Id = lector.GetInt32(0),
                            Descripcion = lector.GetString(1),
                            Monto = lector.GetDecimal(2),
                            Categoria = lector.GetString(3)
                        };

                        return gasto;
                    }
                }
            }
        }

        return null;
    }

    public bool Actualizar(Gasto gasto)
    {
        using (SqlConnection conexion =
            new SqlConnection(CadenaConexion))
        {
            conexion.Open();

            string sql =
                "UPDATE Gastos " +
                "SET Monto = @Monto " +
                "WHERE Id = @Id";

            using (SqlCommand comando =
                new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue(
                    "@Monto", gasto.Monto);

                comando.Parameters.AddWithValue(
                    "@Id", gasto.Id);

                int filasModificadas =
                    comando.ExecuteNonQuery();

                return filasModificadas > 0;
            }
        }
    }

    public bool Eliminar(int id)
    {
        using (SqlConnection conexion =
            new SqlConnection(CadenaConexion))
        {
            conexion.Open();

            string sql =
                "DELETE FROM Gastos WHERE Id = @Id";

            using (SqlCommand comando =
                new SqlCommand(sql, conexion))
            {
                comando.Parameters.AddWithValue("@Id", id);

                int filasEliminadas =
                    comando.ExecuteNonQuery();

                return filasEliminadas > 0;
            }
        }
    }
}