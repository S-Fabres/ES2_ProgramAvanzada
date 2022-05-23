using ServetSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ES2_ProgramAvanzada.ComunicacionLectura
{
    public class HebraServidor
    {
        public void Ejecutar()
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            ServerSocket server = new ServerSocket(puerto);
            Console.WriteLine("S: Iniciando servidor en puerto {0}", puerto);
            if (server.Iniciar())
            {
                while (true)
                {
                    Console.WriteLine("S: Esperando Cliente...");
                    Socket cliente = server.ObtenerCliente();
                    Console.WriteLine("S: Cliente recibido");

                    ClienteCom clienteCom = new ClienteCom(cliente);

                    HebraCliente clienteThread = new HebraCliente(clienteCom);
                    Thread t = new Thread(new ThreadStart(clienteThread.Ejecutar))
                    {
                        IsBackground = true
                    };
                    t.Start();
                }
            }
            else
            {
                Console.WriteLine("Fail, no se puede levantar server en puerto {0}", puerto);
            }
        }
    }
}
