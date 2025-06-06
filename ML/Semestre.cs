using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Semestre
    {
        public int IdSemestre { get; set; }

        [Required(ErrorMessage = "El campo Descripcion es requerido")]
        [RegularExpression(@"[aA-zZ]", ErrorMessage = "Solo acepto Letras")]
        public string Descripcion { get; set; }
        public string Nombre { get; set; }
        public ML.Grupo Grupo { get; set; }
        public List<object> Semestres { get; set; }

    }
}
