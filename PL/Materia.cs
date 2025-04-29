using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Materia
    {

        public static void Delete()
        {


            Console.WriteLine("Ingresa el ID de la materia: ");
            int IdMateria = Convert.ToInt32(Console.ReadLine());

            ML.Result result = BL.Materia.Delete(IdMateria);

            if (result.Correct) 
            {
                Console.WriteLine("Se elimino correctamente");
            }
            else
            {
                Console.WriteLine(result.ErrorMessage);
            }
        }


 

        public static void GetAll()
        {
            ML.Result result = BL.Materia.GetAll();

            if (result.Correct)
            {
                foreach (ML.Materia materiaDB in result.Objects)
                {
                    Console.WriteLine(materiaDB.IdMateria);
                    Console.WriteLine(materiaDB.Nombre);
                    Console.WriteLine(materiaDB.Descripcion);
                    Console.WriteLine(materiaDB.Creditos);
                }
            }
            else
            {
                Console.WriteLine(result.ErrorMessage);
            }
        }

        public static void Add()
        {
            ML.Materia materia = new ML.Materia();
            Console.WriteLine("Ingresa nombre");
            materia.Nombre = Console.ReadLine();
            Console.WriteLine("Descripción");
            materia.Descripcion = Console.ReadLine();
            Console.WriteLine("Creditos");
            materia.Creditos = Convert.ToDecimal(Console.ReadLine());

            ML.Result resultAdd = BL.Materia.Add(materia);

            if (resultAdd.Correct)
            {
                Console.WriteLine("Materia insertada correctamente");
            }
            else
            {
                Console.WriteLine("Error al insertar: "+resultAdd.ErrorMessage);
            }

        }

    }
}
