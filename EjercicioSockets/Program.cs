using ServerSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioSockets
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Cambio el color de la consola para distinguirla de la del cliente.
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);
            Console.WriteLine("Se inicia la conexión con el servidor en el puerto {0}", puerto);
            ServerSocket servidor = new ServerSocket(puerto);
            if (servidor.iniciar())
            {
                // Conexion exitosa
                Console.WriteLine("Se ha iniciado el servidor");
                while (true)
                {
                    Console.WriteLine("Esperando que se conecte algún cliente ...");
                    Console.WriteLine("");
                    Socket socketCliente = servidor.ObtenerCliente();
                    Console.WriteLine("Se ha conectado un cliente! ");
                    Console.WriteLine("");
                    ClienteCom clienteCom = new ClienteCom(socketCliente);
                    generarComunicacion(clienteCom);
                }
            }
            else
            {
                Console.WriteLine("Error, el puerto {0} ya esta siendo utilizado...", puerto);
            }
        }

        public static void generarComunicacion(ClienteCom clienteCom)
        {
            bool finalizarComunicacion = false;
            while (!finalizarComunicacion)
            {
                Console.WriteLine("Recibiendo mensajes del cliente...");
                string respuesta = clienteCom.Leer();
                if (respuesta != null)
                {
                    Console.WriteLine("El cliente dijo: {0}", respuesta);
                    if (respuesta.ToLower() == "adios")
                    {
                        finalizarComunicacion = true;
                    }
                    else
                    {
                        Console.WriteLine("Ingrese una respuesta para el cliente ...");
                        respuesta = Console.ReadLine().Trim();
                        clienteCom.Escribir(respuesta);
                        if (respuesta.ToLower() == "adios")
                        {
                            finalizarComunicacion = true;
                        }
                    }
                }
                else
                {
                    finalizarComunicacion = true;
                }
                if (finalizarComunicacion == true)
                {
                    clienteCom.Desconectar();
                    Console.WriteLine("¡El cliente se ha pirado!");
                }
            }
        }
    }
}
