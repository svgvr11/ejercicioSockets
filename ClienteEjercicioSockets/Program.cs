using ServerSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteEjercicioSockets
{
    class Program
    {
        static void Main(string[] args)
        {
            // Cambio el color de la consola para distinguirla de la del server.
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Ingresa la IP del equipo con el cual te comunicarás: ");
            string ip = Console.ReadLine();
            ConfigurationManager.AppSettings.Set("puerto", ip);
            Console.WriteLine("Ingresa el puerto al que deseas comunicarte");
            string puerto = Console.ReadLine();
            ConfigurationManager.AppSettings.Set("servidor", puerto);
            Console.WriteLine("Te has conectado al servidor {0} en el puerto {1}", ip, puerto);
            ClienteSocket clienteSocket = new ClienteSocket(ip, Convert.ToInt32(puerto));
            if (clienteSocket.Conectar())
            {
                Console.WriteLine("¡Conexión exitosa... !");
                Console.WriteLine("Si deseas finalizar la comunicación, escribe 'adios' ...");
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Ten cuidado de no añadir espacios en el primer mensaje que envies o todo se ira al carajo!! estas advertido pa....");
                Console.BackgroundColor = ConsoleColor.Magenta;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("");
                generarComunicacionServer(clienteSocket);
            }
            else
            {
                Console.WriteLine("Ha ocurrido un error de comunicación con el servidor");
            }
            Console.ReadKey();
        }
        public static void generarComunicacionServer(ClienteSocket clienteSocket)
        {
            bool finalizarComunicacion = false;
            while (!finalizarComunicacion)
            {
                Console.WriteLine("Envía un mensaje al servidor...");
                string mensaje = Console.ReadLine().Trim();
                clienteSocket.Escribir(mensaje);
                if (mensaje.ToLower() == "adios")
                {
                    finalizarComunicacion = true;
                }
                else
                {
                    mensaje = clienteSocket.Leer();
                    Console.WriteLine("El servidor ha respondido: {0}", mensaje);
                    if (mensaje.ToLower() == "adios")
                    {
                        finalizarComunicacion = true;
                    }
                }
            }
            clienteSocket.Desconectar();
            Console.WriteLine("¡Ha finalizado la comunicación con el servidor!");
        }
    }
}
