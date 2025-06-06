using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Materia
    {
        public int IdMateria { get; set; }


        [Required (ErrorMessage ="El campo Nombre es requerido")]
        [RegularExpression(@"^[aA-zZ]+$", ErrorMessage ="Solo acepto Letras")]
        public string Nombre { get; set; }

        [Required (ErrorMessage ="El campo Descripcion es requerido")]
        public string Descripcion { get; set; }
        public decimal Creditos { get; set; }

      
        public ML.Semestre Semestre { get; set; } //FK
        public List<object> Materias { get; set; }
        public byte[] Imagen { get; set; }
        public List<object> Errores { get; set; }
        public string Fecha { get; set; }
    }
}
