using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PL
{
    internal class Program
    {
        public static string Validar(string[] linea)
        {
            // Usuario.Errores = new List<object>;
            List<string> Errores = new List<string>(); // Linea

            if (!Regex.IsMatch(linea[0], @"^[a-zA-Z]+$"))
            {
                Errores.Add($"No se aceptan numeros en {linea[0]}");
            }


            if(Errores.Count > 0)
            {
                return string.Join(",", Errores);
            }

            return "";

        }

        static void Main(string[] args)
        {
            //Materia.Add();

            // Leer TXT:
            // Read file using StreamReader. Reads file line by line

            // Ruta Absoluta
            string rutaTxt = "C:/Users/digis/Downloads/TestRead.txt";
            List<string> Errores = new List<string>();
            // Usuario.Errores = new List<object> Global

            // Ruta Relativa
            string rutaRelativa = "txt/TestRead.txt";

            using (StreamReader file = new StreamReader(rutaTxt))
            {
                int counter = 0;
                string ln;

                string linea = file.ReadLine();
                while ((ln = file.ReadLine()) != null)
                {
                    // Leer su archivo.

                    string[] lineaArray = ln.Split('|');

                    string erroresEncontrados =  Validar(lineaArray);

                    if(erroresEncontrados != "")
                    {
                        Errores.Add(erroresEncontrados);
                    }
                    
                    Console.WriteLine(ln);
                    counter++;
                }

                foreach(var error in Errores)
                {
                    Console.WriteLine(error);
                }

                file.Close();
                Console.WriteLine($"File has {counter} lines.");
            }

        }
    }
}
