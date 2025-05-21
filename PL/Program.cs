using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Materia.Add();

            // Leer TXT:
            // Read file using StreamReader. Reads file line by line

            string rutaTxt = "C:/Users/digis/Downloads/TestRead.txt";
            using (StreamReader file = new StreamReader(rutaTxt))
            {
                int counter = 0;
                string ln;

                while ((ln = file.ReadLine()) != null)
                {
                    ML.Usuario usuario = new ML.Usuario();
                    Console.WriteLine(ln);
                    counter++;
                }
                file.Close();
                Console.WriteLine($"File has {counter} lines.");
            }
        }
    }
}
