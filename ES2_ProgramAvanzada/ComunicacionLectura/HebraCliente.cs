using LecturaModel;
using LecturaModel.DAL;
using ServetSocketUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES2_ProgramAvanzada.ComunicacionLectura
{
    class HebraCliente
    {
        private static ILecturasDAL lecturasDAL = LecturasDALArchivos.GetInstancia();

        private ClienteCom clienteCom;

        public HebraCliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }

        public void Ejecutar()
        {
            clienteCom.Escribir("Ingrese código de lectura :");
            string codLectura = clienteCom.Leer();
            clienteCom.Escribir("Ingrese identificador del medidor :");
            string idMedidor = clienteCom.Leer();
            clienteCom.Escribir("Ingrese fecha en formato DD-MM-AAAA :");
            string fecha = clienteCom.Leer();
            clienteCom.Escribir("Ingrese lectura actual :");
            string lecturaActual = clienteCom.Leer();

            Lectura lectura = new Lectura()
            {
                CodLectura = codLectura,
                IdMedidor = idMedidor,
                Fecha = fecha,
                LecturaActual = lecturaActual
            };
            lock (lecturasDAL)
            {
                lecturasDAL.IngresarLectura(lectura);
            }
            clienteCom.Desconectar();
        }
    }
}
