using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Materia
    {

        public static ML.Result Delete(int IdMateria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    context.Open();

                    string query = "DELETE FROM Materia WHERE IdMateria = @IdMateria";

                    //SqlCommand cmd = new SqlCommand(query, context);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;

                    cmd.Parameters.AddWithValue("@IdMateria", IdMateria);

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar la materia";
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


        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            result.Objects = new List<object>();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    string query = "SELECT IdMateria, Nombre, Descripcion, Creditos FROM Materia";

                    SqlCommand command = new SqlCommand(query, context);
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {

                        foreach (DataRow row in dataTable.Rows)
                        {
                            ML.Materia materia = new ML.Materia();

                            materia.IdMateria = Convert.ToInt32(row[0]);
                            materia.Nombre = row[1].ToString();
                            materia.Descripcion = row[2].ToString();
                            materia.Creditos = Convert.ToDecimal(row[3]);

                            result.Objects.Add(materia);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrio un error al consultar los datos";
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

        public static ML.Result Add(ML.Materia materia)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    SqlCommand command = new SqlCommand("MateriaAdd", context);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Nombre", materia.Nombre);
                    command.Parameters.AddWithValue("@Descripcion", materia.Descripcion);
                    command.Parameters.AddWithValue("@Creditos", materia.Creditos);

                    context.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo agregar el registro";
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


        public static ML.Result DeleteSP(int IdMateria)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Connection.GetConnection()))
                {
                    SqlCommand cmd = new SqlCommand("MateriaDelete", context);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@IdMateria", IdMateria);

                    context.Open();

                    int filasAfectadas = cmd.ExecuteNonQuery();

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar la materia";
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



        public static ML.Result AddEF(ML.Materia Materia)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL_EF.JGuevaraProgramacionNCapasAbriEntities context = new DL_EF.JGuevaraProgramacionNCapasAbriEntities())
                {
                    DL_EF.Materia materia = new DL_EF.Materia();
                    materia.Nombre = Materia.Nombre;
                    materia.Descripcion = Materia.Descripcion;
                    materia.Creditos = Materia.Creditos;
                    context.Materias.Add(materia);
                    //INSERT INTO Materia VALUES ()
                    int filasAfectadas = context.SaveChanges();

                    if (filasAfectadas > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se inserto";
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

        //public static ML.Result GetAllLINQ()
        //{
        //    ML.Result result = new ML.Result();

        //    try
        //    {

        //        using (DL_EF.JGuevaraProgramacionNCapasAbriEntities context = new DL_EF.JGuevaraProgramacionNCapasAbriEntities())
        //        {

        //            var query = (
        //                            from materia in context.Materias
        //                            select new
        //                            {
        //                                IdMateria = materia.IdMateria,
        //                                Nombre = materia.Nombre,
        //                                Descripcion = materia.Descripcion,
        //                                Creditos = materia.Creditos,
        //                                IdSemestre = materia.IdSemestre
        //                            }
        //                        ).ToList();

        //            if (query.Count > 0)
        //            {
        //                result.Objects = new List<object>();
        //                foreach (var itemMateria in query)
        //                {
        //                    ML.Materia materia = new ML.Materia();
        //                    materia.IdMateria = itemMateria.IdMateria;

        //                    //ternario

        //                    materia.Nombre = itemMateria.Nombre == null ? "" : itemMateria.Nombre;
        //                    if (itemMateria.Nombre == null)
        //                    {
        //                        materia.Nombre = "";
        //                    }
        //                    else
        //                    {
        //                        materia.Nombre = itemMateria.Nombre;

        //                    }

        //                    if(itemMateria.IdSemestre == null)
        //                    {
        //                        materia.IdSemestre = 0;
        //                    } else
        //                    {

        //                        materia.IdSemestre = itemMateria.IdSemestre.Value; //int? null
        //                    }


        //                    materia.Descripcion = itemMateria.Descripcion;
        //                    materia.Creditos = Convert.ToDecimal(itemMateria.Creditos);

        //                    result.Objects.Add(materia);
        //                }

        //                result.Correct = true;

        //            }
        //            else
        //            {
        //                result.ErrorMessage = "No se encontro información";
        //                result.Correct = false;
        //            }

        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = e.Message;
        //        result.Ex = e;
        //    }

        //    return result;
        //}

        public static ML.Result DeleteLINQ(int IdMateria)
        {
            ML.Result result = new ML.Result();

            try
            {

                using (DL_EF.JGuevaraProgramacionNCapasAbriEntities context = new DL_EF.JGuevaraProgramacionNCapasAbriEntities())
                {

                    var materiaGet = (from materia in context.Materias
                                 where materia.IdMateria == IdMateria
                                 select materia).SingleOrDefault();

                    if(materiaGet != null)
                    {
                        context.Materias.Remove(materiaGet);
                        context.SaveChanges();
                    }
                    else
                    {
                        result.ErrorMessage = "No se encontro la materia";
                        result.Correct = false;
                    }
                }

            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
                result.Ex = e;
            }

            return result;
        }

        public static ML.Result GetAllSP()
        {
            ML.Result result = new ML.Result();

            try
            {

                using (DL_EF.JGuevaraProgramacionNCapasAbriEntities context = new DL_EF.JGuevaraProgramacionNCapasAbriEntities())
                {
                    var materiaList = context.MateriaGetAll().ToList();

                    if (materiaList.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var itemMateria in materiaList)
                        {
                            ML.Materia materia = new ML.Materia();
                            materia.Semestre = new ML.Semestre();

                            materia.IdMateria = itemMateria.IdMateria;
                            materia.Nombre = itemMateria.Nombre;
                            materia.Descripcion = itemMateria.Descripcion;
                            materia.Creditos = Convert.ToDecimal(itemMateria.Creditos);
                            materia.Semestre.IdSemestre = (int) itemMateria.IdSemestre;
                            materia.Semestre.Descripcion = itemMateria.Semestre;



                            result.Objects.Add(materia);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.ErrorMessage = "SIn resultados";
                        result.Correct = false;
                    }
                }

            }
            catch (Exception e)
            {
                result.Correct = false;
                result.ErrorMessage = e.Message;
                result.Ex = e;
            }

            return result;
        }

    }
}
