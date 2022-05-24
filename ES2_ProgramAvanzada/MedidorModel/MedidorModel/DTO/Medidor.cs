using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedidorModel.DTO
{
    public class Medidor
    {
        private string idMedidor;
        private string lecturaInicial;
        private string ultimaLectura;

        public string IdMedidor { get => idMedidor; set => idMedidor = value; }
        public string LecturaInicial { get => lecturaInicial; set => lecturaInicial = value; }
        public string UltimaLectura { get => ultimaLectura; set => ultimaLectura = value; }

        public override string ToString()
        {
            return idMedidor + " " + lecturaInicial + " " + ultimaLectura;
        }

    }
}
