using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SL_WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Materia" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Materia.svc or Materia.svc.cs at the Solution Explorer and start debugging.
    public class Materia : IMateria
    {
        public void DoWork()
        {
        }

        public SL_WCF.Result Delete(int IdMateria)
        {
            ML.Result resultDelete = BL.Materia.DeleteEFSP(IdMateria);

            //SL_WCF.Result resultSL = new SL_WCF.Result();
            //resultSL.Correct = resultDelete.Correct;
            //resultSL.ErrorMessage = resultDelete.ErrorMessage;
            //resultSL.Ex = resultDelete.Ex;
            //resultSL.Object = resultDelete.Object;
            //resultSL.Objects = resultDelete.Objects;

            return new SL_WCF.Result
            {
                Correct = resultDelete.Correct,
                ErrorMessage = resultDelete.ErrorMessage,
                Ex = resultDelete.Ex,
                Object = resultDelete.Object,
                Objects = resultDelete.Objects,
            };

        }
    }
}
