using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ofertaWPF.Models
{
    public enum IdTrimestre
    {
        FebreroAbril = 1,
        MayoJulio = 2,
        AgostoOctubre = 3,
        NoviembreEnero = 4

    }
    public class Trimestre
    {
        public int Año { get; set; }
        public string Meses { get; set; }
        public int IdTrimestre { get; set; }
        public int IdOferta { get; set; }

        
    }
}
