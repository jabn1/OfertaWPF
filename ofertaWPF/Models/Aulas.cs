using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ofertaWPF.Models
{
    public class Aulas : List<HorarioAula>   
    {
        public bool HasAula(string aula)
        {
            foreach (var element in this)
            {
                if (element.aula == aula) { return true; }
            }
            return false;
        }
        public int SearchAula(string aula)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this[i].aula == aula) { return i; }
            }
            return -1;
        }
    }
}
