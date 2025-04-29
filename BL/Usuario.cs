using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {

        public static ML.Result Add(ML.Usuario Usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    context.Open();

                    string query = "INSERT INTO Usuario VALUES (@Nombre, @Correo)";

                    SqlCommand sqlCommand = new SqlCommand(query, context);

                    sqlCommand.Parameters.AddWithValue("@Nombre", Usuario.Nombre);
                    sqlCommand.Parameters.AddWithValue("@Correo", Usuario.Correo);

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        // inserto correctamente
                        result.Correct = true;
                    }
                    else
                    {
                        // Ocurrio un error
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al insertar al usuario";
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }


        public static bool Delete(int IdUsuario)
        {

            bool usuarioEliminado = false;
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    context.Open();

                    string query = "DELETE FROM Usuario WHERE IdUsuario = @IdUsuario";

                    SqlCommand sqlCommand = new SqlCommand(query, context);

                    sqlCommand.Parameters.AddWithValue("@IdUsuario", IdUsuario);

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        usuarioEliminado = true;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return usuarioEliminado;
        }

    }
}
