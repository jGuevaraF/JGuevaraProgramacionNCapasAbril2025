using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Grupo
    {
        public static ML.Result GetByIdSemestre(int IdSemestre)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL_EF.JGuevaraProgramacionNCapasAbriEntities context = new DL_EF.JGuevaraProgramacionNCapasAbriEntities())
                {
                    var listaGrupos = context.GrupoGetByIdSemestre(IdSemestre).ToList();

                    if(listaGrupos.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach(var itemGrupo in listaGrupos)
                        {
                            ML.Grupo grupo = new ML.Grupo();

                            grupo.IdGrupo = itemGrupo.IdGrupo;
                            grupo.Nombre = itemGrupo.Nombre;

                            result.Objects.Add(grupo);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            } catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
