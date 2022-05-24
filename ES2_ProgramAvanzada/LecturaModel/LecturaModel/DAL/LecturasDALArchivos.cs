using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecturaModel.DAL
{
    public class LecturasDALArchivos : ILecturasDAL
    {
        private LecturasDALArchivos()
        {

        }

        private static LecturasDALArchivos instancia;

        public static ILecturasDAL GetInstancia()
        {
            if (instancia== null)
            {
                instancia = new LecturasDALArchivos();
            }
            return instancia;
        }

        private static readonly string url = Directory.GetCurrentDirectory();
        private static readonly string archivo = url + "/lecturas.txt";
        public void IngresarLectura(Lectura lectura)
        {
            try
            {
                using (StreamWriter write = new StreamWriter(archivo, true))
                {
                    write.WriteLine(lectura.CodLectura + ";" + lectura.IdMedidor + ";" + lectura.Fecha + ";" + lectura.LecturaActual);
                    write.Flush();
                }
            } catch (Exception)
            {

            }
        }

        public List<Lectura> ObtenerLecturas()
        {
            List<Lectura> lista = new List<Lectura>();
            try
            {
                using (StreamReader reader = new StreamReader(archivo))
                {
                    string texto = "";
                    do
                    {
                        texto = reader.ReadLine();
                        if (texto != null)
                        {
                            string[] arr = texto.Trim().Split(';');
                            Lectura lectura = new Lectura()
                            {
                                CodLectura = arr[0],
                                IdMedidor = arr[1],
                                Fecha = arr[2],
                                LecturaActual = arr[3]
                            };
                            lista.Add(lectura);
                        }
                    } while (texto != null);
                }
            } catch (Exception)
            {
                lista = null;
            }
            return lista;
        }
    }
}
