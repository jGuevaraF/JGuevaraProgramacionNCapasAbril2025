using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Result
    {
        public bool Correct { get; set; }
        public string ErrorMessage { get; set; }
        public Exception Ex { get; set; }
        public object Object { get; set; }  // Guardar 1 registro
        public List<object> Objects { get; set; }  // Guardar mas de 1 registro
    }
}
