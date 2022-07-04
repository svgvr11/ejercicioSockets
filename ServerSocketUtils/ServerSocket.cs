using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSocketUtils
{
    public class ServerSocket
    {
        private int puerto;
        private Socket servidor;

        public ServerSocket(int puerto)
        {
            this.puerto = puerto;
        }

        public bool iniciar()
        {
            try
            {
                servidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                servidor.Bind(new IPEndPoint(IPAddress.Any, puerto));
                // Esto representa la cantidad de usuarios en espera
                servidor.Listen(10);
                return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }

        public Socket ObtenerCliente()
        {
            return servidor.Accept();
        }

        public void Cerrar()
        {
            try
            {
                servidor.Close();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
