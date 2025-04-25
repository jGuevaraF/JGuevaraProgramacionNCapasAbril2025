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

        public static bool Add(ML.Usuario usuario)
        {
            bool registroExitoso = false;
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    context.Open();

                    string query = "INSERT INTO Usuario VALUES (@Nombre, @Correo)";

                    SqlCommand sqlCommand = new SqlCommand(query, context);

                    sqlCommand.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                    sqlCommand.Parameters.AddWithValue("@Correo", usuario.Correo);

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        // inserto correctamente
                        registroExitoso = true;
                    }
                    else
                    {
                        // Ocurrio un error
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return registroExitoso;
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
