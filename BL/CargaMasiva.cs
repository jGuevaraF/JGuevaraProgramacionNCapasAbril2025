using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BL
{
    public class CargaMasiva
    {

        public static void Validar(string[] campos, List<string> Errores, List<string> Correctos)
        {
            string error = string.Empty;

            if (!int.TryParse(campos[0], out int IdMateria))
            {
                error += "El IdMateria no es un numero|";
            }

            if (!Regex.IsMatch(campos[1], @"^[a-zA-Z ]+$"))
            {
                error += $"No puedes contener numeros en: {campos[1]}|";
            }

            if (!Regex.IsMatch(campos[2], @"^[a-zA-Z ]+$"))
            {
                error += $"No puedes contener numeros en: {campos[2]}";
            }

            //if (campos[1].ToString() != null)
            //{
            //    error += "Nombre es muy grande|";
            //}

            //if (campos[2].ToString() != null)
            //{
            //    error += "Descripcion es muy grande|";
            //}



            if (error.Length > 0)
            {
                Errores.Add(error);
            }
            else
            {
                Correctos.Add("El registro " + campos[1] + " es correcto");
            }
        }


        public static ML.Result LeerExcel(string conexionString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(conexionString))
                {
                    context.Open();
                    string query = "SELECT * FROM [Sheet1$]";

                    OleDbCommand oleDbCommand = new OleDbCommand();
                    oleDbCommand.Connection = context;
                    oleDbCommand.CommandText = query;

                    OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(oleDbCommand);

                    DataTable dataTable = new DataTable();
                    oleDbDataAdapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (DataRow itemOledb in dataTable.Rows)
                        {
                            ML.Materia materia = new ML.Materia();
                            materia.IdMateria = Convert.ToInt32(itemOledb[0]);
                            materia.Nombre = itemOledb[1].ToString();
                            materia.Descripcion = itemOledb[2].ToString();

                            result.Objects.Add(materia);

                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo leer el archivo";
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
    }
}
