using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DL
{
    public class Connection
    {
        public static string GetConnection()
        {
            //string conexion = "Data Source=.;Initial Catalog=JGuevaraProgramacionNCapasAbril2025;User ID=sa;Password=pass@word1;Encrypt=False";
            //string conexion = "Data Source=.;Initial Catalog=OArredondoProgramacionNCapasAbril;User ID=sa;Password=pass@word1;Encrypt=False";
            //string conexion = "Data Source=ALIEN3;Initial Catalog=JGuevaraProgramacionNCapasAbri;User ID=sa;Password=Qwerty123456$$$#;Encrypt=False";

            string conexion = ConfigurationManager.ConnectionStrings["JGuevaraProgramacionNCapas"].ToString();
            return conexion;
        }
    }
}

