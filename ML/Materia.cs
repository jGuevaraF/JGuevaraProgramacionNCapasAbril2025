using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Materia
    {
        public int IdMateria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Creditos { get; set; }
        public ML.Semestre Semestre { get; set; } //FK
        public List<object> Materias { get; set; }
    }
}
