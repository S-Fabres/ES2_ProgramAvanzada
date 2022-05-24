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
            int valor = 0;
            if (medidores is null)
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
            else
            {
                
                medidores = medidoresDAL.ObtenerMedidores();
                foreach (Medidor medidor in medidores)
                {
                    if (auxMedidor.IdMedidor == medidor.IdMedidor)
                    {
                        valor = 1;
                    }
                    else
                    {
                        valor = 0;
                    }
                        
                }
                if (valor == 1)
                {
                    Console.WriteLine("Medidor ingresado anteriormente");
                    
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
        static void IngresarLectura()
        {
            Console.WriteLine("Ingrese codigo de lectura :");
            string codLectura = Console.ReadLine().Trim();
            
            int auxiliar = 0;
            Medidor auxMedidor = new Medidor();
            Lectura auxLectura = new Lectura();
            auxLectura.CodLectura = codLectura;
            List<Lectura> lecturas = new List<Lectura>();
            lock (lecturasDAL)
            {
                lecturas = lecturasDAL.ObtenerLecturas();
            }
            if (lecturas is null)
            {
                Console.WriteLine("Ingrese identificador del medidor :");
                string idMedidor = Console.ReadLine().Trim();
                auxMedidor.IdMedidor = idMedidor;
                List<Medidor> medidores = new List<Medidor>();
                lock (medidoresDAL)
                {
                    medidores = medidoresDAL.ObtenerMedidores();
                }
                foreach (Medidor medidor in medidores)
                {
                    if (auxMedidor.IdMedidor == medidor.IdMedidor)
                    {
                        auxiliar = 1;

                    }

                }
                if (auxiliar == 1)
                {
                    Console.WriteLine("Medidor registrado");
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
                        Console.WriteLine("OK");
                    }
                }
                else
                {
                    Console.WriteLine("El identificador de medidor ingresado no corresponde a ningun medidor registrado");
                }
            }
            else
            {
                foreach (Lectura lectura in lecturas)
                {
                    if (auxLectura.CodLectura == lectura.CodLectura)
                    {
                        auxiliar = 1;
                    }
                }
                if (auxiliar == 1)
                {
                    Console.WriteLine("El código ingresado se encuentra asociado a otra lectura");
                }
                else
                {
                    Console.WriteLine("Ingrese identificador del medidor :");
                    string idMedidor = Console.ReadLine().Trim();
                    auxMedidor.IdMedidor = idMedidor;
                    List<Medidor> medidores = new List<Medidor>();
                    lock (medidoresDAL)
                    {
                        medidores = medidoresDAL.ObtenerMedidores();
                    }
                    foreach (Medidor medidor in medidores)
                    {
                        if (auxMedidor.IdMedidor == medidor.IdMedidor)
                        {
                            auxiliar = 1;

                        }

                    }
                    if (auxiliar == 1)
                    {
                        Console.WriteLine("Medidor registrado");
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
                            Console.WriteLine("OK");
                        }
                    }
                    else
                    {
                        Console.WriteLine("El identificador de medidor ingresado no corresponde a ningun medidor registrado");
                    }

                }
            }
            
            
        }
        static void ObtenerMedidores()
        {
            try
            {


                List<Medidor> medidores = new List<Medidor>();
                lock (medidoresDAL)
                {
                    medidores = medidoresDAL.ObtenerMedidores();
                }
                foreach (Medidor medidor in medidores)
                {
                    Console.WriteLine(medidor);
                }
            } catch (Exception)
            {
                Console.WriteLine("No hay medidores registrados");
            }
        }
    

        static void ObtenerLecturas()
        {
            try
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
            } catch (Exception)
            {
                Console.WriteLine("No hay lecturas registradas");
            }
        }
    }
}
