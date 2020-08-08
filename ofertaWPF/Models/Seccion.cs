using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ofertaWPF.Models
{
    public class Seccion
    {
        
        public string tipo, asignatura, idSec, aula, profesor, lun, mar, mier, jue, vie, sab, area; 
        public string Tipo { get { return tipo; } set { tipo = value; } }
        public string Asignatura { get { return asignatura;} set { asignatura = value; } }
        public string Sec { get { return idSec; } set { idSec = value; } }
        public string Aula { get { return aula; } set { aula = value; } }
        public string Profesor { get { return profesor; } set { profesor = value; } }
        public string Lun { get { return lun; } set { lun = value; } }
        public string Mar { get { return mar; } set { mar = value; } }
        public string Mier { get { return mier; } set { mier = value; } }
        public string Jue { get { return jue; } set { jue = value; } }
        public string Vie { get { return vie; } set { vie = value; } }
        public string Sab { get { return sab; } set { sab = value; } }
        public string Area { get { return area; } set { area = value; } }

        

    }
}
