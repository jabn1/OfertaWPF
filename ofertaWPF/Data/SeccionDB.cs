using ofertaWPF.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ofertaWPF.Data
{
    public class SeccionDB
    {
        public static List<Seccion> GetSecciones(int idOferta)
        {
            List<Seccion> secciones = new List<Seccion>();

            using (SQLiteConnection con = new SQLiteConnection("Data Source=oferta.sqlite;Version=3;"))
            {
                using (SQLiteCommand cmd = new SQLiteCommand(con))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText =
                        "SELECT Seccion.* FROM Oferta " +
                        "INNER JOIN Seccion ON Seccion.IdOferta = Oferta.IdOferta " +
                        "WHERE Oferta.IdOferta = @IdOferta";

                    cmd.Parameters.Add("@IdOferta", DbType.Int32).Value = idOferta;

                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var seccion = new Seccion();
                            seccion.Tipo = reader["tipo"].ToString();
                            seccion.Sec = reader["idSec"].ToString();
                            seccion.Aula = reader["aula"].ToString();
                            seccion.Profesor = reader["profesor"].ToString();
                            seccion.Lun = reader["lun"].ToString();
                            seccion.Mar = reader["mar"].ToString();
                            seccion.Mier = reader["mier"].ToString();
                            seccion.Jue = reader["jue"].ToString();
                            seccion.Vie = reader["vie"].ToString();
                            seccion.Sab = reader["sab"].ToString();
                            seccion.Asignatura = reader["asignatura"].ToString();
                            seccion.Area = reader["area"].ToString();
                            secciones.Add(seccion);
                        }


                    }
                }
            }
            return secciones;
        }

        public static bool SaveSecciones(List<Seccion> secciones, Trimestre trimestre)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    using (SQLiteConnection con = new SQLiteConnection("Data Source=oferta.sqlite;Version=3;"))
                    {
                        using (SQLiteCommand cmd = new SQLiteCommand(con))
                        {
                            cmd.CommandType = CommandType.Text;


                            cmd.CommandText =
                                "INSERT INTO Oferta(IdTrimestre,Año) VALUES " +
                                "(@IdTrimestre,@Año); ";
                            cmd.Parameters.Add("@IdTrimestre", DbType.Int32).Value = trimestre.IdTrimestre;
                            cmd.Parameters.Add("@Año", DbType.Int32).Value = trimestre.Año;

                            con.Open();

                            cmd.ExecuteNonQuery();


                            trimestre.IdOferta = (int)con.LastInsertRowId;


                            cmd.CommandText =
                                "INSERT INTO Seccion(IdOferta,tipo, idSec, aula, profesor, lun, mar, mier, jue, vie, sab, asignatura, area) VALUES " +
                                "(@IdOferta, @tipo, @idSec, @aula, @profesor, @lun, @mar, @mier, @jue, @vie, @sab, @asignatura, @area); ";

                            cmd.Parameters.Add("@IdOferta", DbType.Int32).Value = trimestre.IdOferta;
                            string[] parNames = new string[] { "@tipo", "@idSec", "@aula", "@profesor", "@lun", "@mar", "@mier", "@jue", "@vie", "@sab", "@asignatura", "@area" };

                            foreach (var item in parNames)
                            {
                                cmd.Parameters.Add(item, DbType.String);
                            }


                            foreach (var item in secciones)
                            {
                                int i = 0;
                                cmd.Parameters[parNames[i++]].Value = item.Tipo;
                                cmd.Parameters[parNames[i++]].Value = item.Sec;
                                cmd.Parameters[parNames[i++]].Value = item.Aula;
                                cmd.Parameters[parNames[i++]].Value = item.Profesor;
                                cmd.Parameters[parNames[i++]].Value = item.Lun;
                                cmd.Parameters[parNames[i++]].Value = item.Mar;
                                cmd.Parameters[parNames[i++]].Value = item.Mier;
                                cmd.Parameters[parNames[i++]].Value = item.Jue;
                                cmd.Parameters[parNames[i++]].Value = item.Vie;
                                cmd.Parameters[parNames[i++]].Value = item.Sab;
                                cmd.Parameters[parNames[i++]].Value = item.Asignatura;
                                cmd.Parameters[parNames[i++]].Value = item.Area;

                                cmd.ExecuteNonQuery();

                            }

                        }
                    }
                    ts.Complete();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
