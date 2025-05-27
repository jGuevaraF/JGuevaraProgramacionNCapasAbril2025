using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CargaMasiva
    {

        public static void Validar(ML.Materia materia, List<string> Errores, List<string> Correctos)
        {
            string error = string.Empty;

            if(materia.Nombre != null)
            {
                error += "El nombre es muy largo|";
            }

            if(materia.Creditos > 900)
            {
                error += "Creditos es muy grande|";
            }

            if(error.Length > 0)
            {
                Errores.Add(error);
            } else
            {
                Correctos.Add("El registro "+materia.Nombre+" es correcto");
            }
        }
    }
}
