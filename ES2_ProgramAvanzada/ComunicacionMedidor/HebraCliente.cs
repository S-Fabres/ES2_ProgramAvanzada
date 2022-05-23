using MedidorModel.DAL;
using MedidorModel.DTO;
using ServetSocketUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ES2_ProgramAvanzada.Comunicacion
{
    class HebraCliente
    {
        private static IMedidoresDAL medidoresDAL = MedidoresDALArchivos.GetInstancia();

        private ClienteCom clienteCom;

        public HebraCliente(ClienteCom clienteCom)
        {
            this.clienteCom = clienteCom;
        }

        public void Ejecutar()
        {
            clienteCom.Escribir("Ingrese identificacion del medidor :");
            string idMedidor = clienteCom.Leer();
            clienteCom.Escribir("Ingrese lectura inicial :");
            string lecturaInicial = clienteCom.Leer();
            clienteCom.Escribir("Ingrese ultima lectura ingresada");
            string ultimaLectura = clienteCom.Leer();

            Medidor medidor = new Medidor()
            {
                IdMedidor = idMedidor,
                LecturaInicial = lecturaInicial,
                UltimaLectura = ultimaLectura
            };
            lock (medidoresDAL)
            {
                medidoresDAL.AgregarMedidor(medidor);
            }
            clienteCom.Desconectar();
        }
    }
}
