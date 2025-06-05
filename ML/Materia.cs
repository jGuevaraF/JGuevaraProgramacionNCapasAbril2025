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
        [Key]
        public int IdMateria { get; set; }

        [Required(ErrorMessage = "El campo Nombre es requerido")]
        [MaxLength(50, ErrorMessage = "No puede tener mas de 50 caracteres")]
        [DataType(DataType.Text, ErrorMessage = "Tipo de dato incompatible")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Solo se aceptan letras.")]
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Creditos { get; set; }
        public ML.Semestre Semestre { get; set; } //FK
        public List<object> Materias { get; set; }
        public byte[] Imagen { get; set; }
        public List<object> Errores { get; set; }
        public string Fecha { get; set; }
    }
}
