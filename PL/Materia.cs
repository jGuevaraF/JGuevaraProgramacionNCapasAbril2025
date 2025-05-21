using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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

            //ML.Result result = BL.Materia.Delete(IdMateria);
            ML.Result resultDelete = BL.Materia.DeleteSP(IdMateria);

            if (resultDelete.Correct) 
            {
                Console.WriteLine("Se elimino correctamente");
            }
            else
            {
                Console.WriteLine(resultDelete.ErrorMessage);
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
                    Console.WriteLine(materiaDB.Semestre.IdSemestre);
                    Console.WriteLine(materiaDB.Semestre.Descripcion);
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
            materia.Semestre = new ML.Semestre();

            Console.WriteLine("Ingresa nombre");
            materia.Nombre = Console.ReadLine();
            Console.WriteLine("Descripción");
            materia.Descripcion = Console.ReadLine();
            Console.WriteLine("Creditos");
            materia.Creditos = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Ingresa el ID semetres");
            materia.Semestre.IdSemestre = Convert.ToInt32(Console.ReadLine());


            //ML.Result resultAdd = BL.Materia.Add(materia);
            //ML.Result resultAdd = BL.Materia.AddEF(materia);
            ML.Result resultAdd = BL.Materia.AddEFSP(materia);

            if (resultAdd.Correct)
            {
                Console.WriteLine("Materia insertada correctamente");
            }
            else
            {
                Console.WriteLine("Error al insertar: "+resultAdd.ErrorMessage);
            }

        }

        //public static void GetAllLINQ()
        //{
        //    ML.Result resultGetAll = BL.Materia.GetAllLINQ();

        //    if (resultGetAll.Correct)
        //    {
        //        foreach (ML.Materia materia in resultGetAll.Objects)
        //        {
        //            Console.WriteLine("Id: "+materia.IdMateria+" Nombre: "+materia.Nombre+" Descripción: "+materia.Descripcion+" Creditos: "+materia.Creditos);
        //        }
        //    }
        //}



        public static void DeleteLINQ(int IdMateria)
        {
            ML.Result resultDelete = BL.Materia.DeleteLINQ(IdMateria);

            if (resultDelete.Correct)
            {
                Console.WriteLine("La materia se elimino");
            }
        }

    }
}
