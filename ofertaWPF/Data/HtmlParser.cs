using ofertaWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ofertaWPF.Data
{
    public class HtmlParser
    {
        public static List<Seccion> HtmlParse(string path)
        {
            string[] html;
            try
            {
                html = System.IO.File.ReadAllLines(path);
            }
            catch 
            {
                return null;             
            }
            var data = new List<string>();

            foreach (var line in html)
            {
                string dato = "";
                bool estado = false;
                foreach (var thing in line)
                {
                    if (thing == '>') { estado = true; continue; }
                    else if (estado & thing == '<') { break; }
                    if (estado)
                    {
                        dato += thing.ToString();
                    }
                }
                data.Add(dato.Trim());
            }

            var secciones = new List<Seccion>();
            var areas = new List<string>() { "CIENCIAS BASICAS Y AMBIENTALES (CB)", "CIENCIAS DE LA SALUD (SA)", "CIENCIAS SOCIALES Y HUMANIDADES (SH)", "ECONOMIA Y NEGOCIOS (NG)", "INGENIERIAS  (IN)" };
            var tipos = new List<string>() { "L", "T", "E" };
            string asignatura = "", area = "";
            bool estadoAsig = false;
            bool estadoSec = false;
            bool estadoCheckAsig = false;
            int contadorSec = 0;
            Seccion unaSeccion = null;
            foreach (var line in data)
            {
                if (areas.Contains(line))
                {
                    area = line;
                    continue;
                }
                if (estadoCheckAsig)
                {
                    if (line == "") { continue; }
                    else if (tipos.Contains(line.ToUpper()))
                    {
                        estadoCheckAsig = false;
                    }
                    else
                    {
                        asignatura = line;
                        estadoCheckAsig = false;
                        continue;
                    }
                }
                if (tipos.Contains(line.ToUpper()))
                {
                    unaSeccion = new Seccion();
                    unaSeccion.Tipo = line;
                    estadoSec = true;
                    continue;
                }
                if (estadoSec & contadorSec < 9)
                {
                    if (contadorSec == 0) { unaSeccion.Sec = line; }
                    else if (contadorSec == 1) { unaSeccion.Aula = line; }
                    else if (contadorSec == 2)
                    {
                        unaSeccion.Profesor = line.Replace("&#209;", "Ñ");
                        while (true)
                        {
                            if (unaSeccion.Profesor.Contains("  "))
                            {
                                unaSeccion.Profesor = unaSeccion.Profesor.Replace("  ", " ");
                            }
                            else { break; }
                        }
                    }
                    else if (contadorSec == 3) { unaSeccion.Lun = line; }
                    else if (contadorSec == 4) { unaSeccion.Mar = line; }
                    else if (contadorSec == 5) { unaSeccion.Mier = line; }
                    else if (contadorSec == 6) { unaSeccion.Jue = line; }
                    else if (contadorSec == 7) { unaSeccion.Vie = line; }
                    else if (contadorSec == 8)
                    {
                        unaSeccion.Sab = line;
                        unaSeccion.Area = area;
                        unaSeccion.Asignatura = asignatura.Replace("&#209;", "Ñ");
                        secciones.Add(unaSeccion);
                        unaSeccion = new Seccion();
                        estadoSec = false;
                        estadoCheckAsig = true;
                        contadorSec = 0;
                        continue;
                    }
                    contadorSec++;
                    continue;
                }
                if (line == "Cr")
                {
                    estadoAsig = true;
                    continue;

                }
                if (estadoAsig & line != "")
                {
                    asignatura = line;
                    estadoAsig = false;
                    continue;
                }
                
            }
            return secciones;
        }
    }
}
