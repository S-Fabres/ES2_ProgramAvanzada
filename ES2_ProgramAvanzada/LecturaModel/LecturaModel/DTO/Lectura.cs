using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecturaModel
{
    public class Lectura
    {
        private string codLectura;
        private string idMedidor;
        private string fecha;
        private string lecturaActual;

        public string CodLectura { get => codLectura; set => codLectura = value; }
        public string IdMedidor { get => idMedidor; set => idMedidor = value; }
        public string Fecha { get => fecha; set => fecha = value; }
        public string LecturaActual { get => lecturaActual; set => lecturaActual = value; }

        public override string ToString()
        {
            return codLectura + " " + idMedidor + " " + fecha + " " + lecturaActual;
        }

    }
}
