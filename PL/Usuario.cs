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

            bool registroExitoso = BL.Usuario.Add(usuario);

            if (registroExitoso)
            {
                Console.WriteLine("Se agrego correctamente");
            }
            else
            {
                Console.WriteLine("No se pudo agregar al usuario.");
            }
        }



        public static void Delete()
        {
            Console.WriteLine("Ingresa el id del usuario");

            //int IdUsuario = int.Parse(Console.ReadLine());
            int IdUsuario = Convert.ToInt32(Console.ReadLine());

            bool registroEliminado = BL.Usuario.Delete(IdUsuario);

            if (registroEliminado)
            {
                Console.WriteLine("Se elimino correctamente");
            }
            else
            {
                Console.WriteLine("No se pudo eliminar al usuario.");
            }
        }
    }
}
