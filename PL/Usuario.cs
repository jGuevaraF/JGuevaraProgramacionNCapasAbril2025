using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Usuario
    {
        public void Add()
        {
            ML.Usuario usuario = new ML.Usuario();
            Console.WriteLine("Dame el nombre del Usuario: ");
            usuario.Nombre = Console.ReadLine();

            Console.WriteLine("Dame el correo");
            usuario.Correo = Console.ReadLine();
        }
    }
}
