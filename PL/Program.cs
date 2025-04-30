using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ingresa Id");
            int IdMateria = Convert.ToInt16(Console.ReadLine());
            PL.Materia.DeleteLINQ(IdMateria);
        }
    }
}
