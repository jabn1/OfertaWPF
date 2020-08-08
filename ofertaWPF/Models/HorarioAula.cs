using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ofertaWPF.Models
{
    public class HorarioAula
    {
        public string aula;
        public List<string> horario = new List<string>();
        public List<string> horarioDisplay = new List<string>();

        public string Aula { get { return aula; } }
        public string H7 { get { return horarioDisplay[0]; } }
        public string H8 { get { return horarioDisplay[1]; } }
        public string H9 { get { return horarioDisplay[2]; } }
        public string H10 { get { return horarioDisplay[3]; } }
        public string H11 { get { return horarioDisplay[4]; } }
        public string H12 { get { return horarioDisplay[5]; } }
        public string H13 { get { return horarioDisplay[6]; } }
        public string H14 { get { return horarioDisplay[7]; } }
        public string H15 { get { return horarioDisplay[8]; } }
        public string H16 { get { return horarioDisplay[9]; } }
        public string H17 { get { return horarioDisplay[10]; } }
        public string H18 { get { return horarioDisplay[11]; } }
        public string H19 { get { return horarioDisplay[12]; } }
        public string H20 { get { return horarioDisplay[13]; } }
        public string H21 { get { return horarioDisplay[14]; } }
    }
}
