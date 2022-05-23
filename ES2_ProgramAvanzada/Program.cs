using ES2_ProgramAvanzada.Comunicacion;
using LecturaModel;
using LecturaModel.DAL;
using MedidorModel.DAL;
using MedidorModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ES2_ProgramAvanzada
{
    class Program
    {
        //private static IMedidoresDAL medidoresDAL = new MedidoresDALArchivos();
        private static IMedidoresDAL medidoresDAL = MedidoresDALArchivos.GetInstancia();
        private static ILecturasDAL lecturasDAL = LecturasDALArchivos.GetInstancia();
        static bool Menu()
        {
            bool continuar = true;

            Console.WriteLine("¿Qué quiere hacer?");
            Console.WriteLine(" 1. Agregar medidor \n 2. Ingresar lectura \n 3. ObtenerMedidores \n 4. ObtenerLecturas \n 0. Salir");
            switch (Console.ReadLine().Trim())
            {
                case "1": AgregarMedidor();
                    break;
                case "2": IngresarLectura();
                    break;
                case "3": ObtenerMedidores();
                    break;
                case "4": ObtenerLecturas();
                    break;
                case "0": continuar = false;
                    break;
                default: Console.WriteLine("Ingrese de nuevo");
                    break;
            }
            return continuar;
        }

        static void Main(string[] args)
        {
            HebraServidor hebra = new HebraServidor();
            Thread t = new Thread(new ThreadStart(hebra.Ejecutar))
            {
                IsBackground = true
            };
            t.Start();

            while (Menu()) ;
        }


        static void AgregarMedidor()
        {
            Console.WriteLine("Ingrese identificador del medidor :");
            string idMedidor = Console.ReadLine().Trim();
            Medidor auxMedidor = new Medidor();
            auxMedidor.IdMedidor = idMedidor;
            auxMedidor.LecturaInicial = "";
            auxMedidor.UltimaLectura = "";
            List<Medidor> medidores = medidoresDAL.ObtenerMedidores();
            //Console.WriteLine("OK hasta aquí");
            if (medidores!=null)
            {
                foreach (Medidor medidor in medidores)
                {
                    if (auxMedidor.IdMedidor.Equals(medidor.IdMedidor))
                    {
                        Console.WriteLine("Medidor se encuentra registrado anteriormente");
                    }
                    else
                    {
                        Console.WriteLine("Ingrese lectura inicial :");
                        string lecturaInicial = Console.ReadLine().Trim();
                        Console.WriteLine("Ingrese ultima lectura :");
                        string ultimaLectura = Console.ReadLine().Trim();

                        Medidor aMedidor = new Medidor()
                        {
                            IdMedidor = idMedidor,
                            LecturaInicial = lecturaInicial,
                            UltimaLectura = ultimaLectura
                        };
                        lock (medidoresDAL)
                        {
                            medidoresDAL.AgregarMedidor(aMedidor);
                            Console.WriteLine("OK");
                        }
                    }
                }      
            }
            else
            {
                Console.WriteLine("Ingrese lectura inicial :");
                string lecturaInicial = Console.ReadLine().Trim();
                Console.WriteLine("Ingrese ultima lectura :");
                string ultimaLectura = Console.ReadLine().Trim();

                Medidor aMedidor = new Medidor()
                {
                    IdMedidor = idMedidor,
                    LecturaInicial = lecturaInicial,
                    UltimaLectura = ultimaLectura
                };
                lock (medidoresDAL)
                {
                    medidoresDAL.AgregarMedidor(aMedidor);
                    Console.WriteLine("OK");
                }

            }

        }
        static void IngresarLectura()
        {
            Console.WriteLine("Ingrese codigo de lectura :");
            string codLectura = Console.ReadLine().Trim();
            Console.WriteLine("Ingrese identificador del medidor :");
            string idMedidor = Console.ReadLine().Trim();
            Medidor auxMedidor = new Medidor();
            auxMedidor.IdMedidor = idMedidor;
            List<Medidor> medidores = new List<Medidor>();
            lock (medidoresDAL)
            {
                medidores = medidoresDAL.ObtenerMedidores();
            }
            foreach (Medidor medidor in medidores)
            {
                if (auxMedidor.IdMedidor.Equals(medidor.IdMedidor))
                {
                    Console.WriteLine("Ingrese fecha en formato DD-MM-AAAA :");
                    string fecha = Console.ReadLine().Trim();
                    Console.WriteLine("Ingrese lectura actual :");
                    string lecturaActual = Console.ReadLine().Trim();
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
                }
                else
                {
                    Console.WriteLine("El medidor ingresado no se encuentra registrado");
                }
            }
            
        }
        static void ObtenerMedidores()
        {
            List<Medidor> medidores= new List<Medidor>();
            

            lock (medidoresDAL)
            {
                medidores = medidoresDAL.ObtenerMedidores();
            }
            foreach (Medidor medidor in medidores)
            {
                Console.WriteLine(medidor);
            }
        
        }
    

        static void ObtenerLecturas()
        {
            List<Lectura> lecturas = new List<Lectura>();
            lock (lecturasDAL)
            {
                lecturas = lecturasDAL.ObtenerLecturas();

            }
            foreach (Lectura lectura in lecturas)
            {
                Console.WriteLine(lectura);
            }
        }
    }
}
