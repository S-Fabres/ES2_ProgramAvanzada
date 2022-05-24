using MedidorModel.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidorModel.DAL
{
    public class MedidoresDALArchivos : IMedidoresDAL
    {
        private MedidoresDALArchivos()
        {

        }

        private static MedidoresDALArchivos instancia;

        public static IMedidoresDAL GetInstancia()
        {
            if( instancia== null)
            {
                instancia = new MedidoresDALArchivos();

            }
            return instancia;
        }

        private static string url = Directory.GetCurrentDirectory();
        private static string archivo = url + "/medidores.txt";
        public void AgregarMedidor(Medidor medidor)
        {
            try
            {
                using (StreamWriter write = new StreamWriter(archivo, true))
                {
                    write.WriteLine(medidor.IdMedidor + ";" + medidor.LecturaInicial + ";" + medidor.UltimaLectura);
                    write.Flush();
                }
            }catch (Exception)
            {

            }
        }

        public List<Medidor> ObtenerMedidores()
        {
             List<Medidor> lista = new List<Medidor>();
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
                            Medidor medidor = new Medidor()
                            {
                                IdMedidor = arr[0],
                                LecturaInicial = arr[1],
                                UltimaLectura = arr[2]
                            };
                            lista.Add(medidor);
                        }
                    } while (texto != null);
                }
            }catch (Exception)
            {
                lista = null;
            }
            return lista;
        }
    }
}
