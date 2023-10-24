using System;
using System.Collections;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using Serialization;
using System.Text;
using System.Threading.Tasks;
using Serialization;
using Newtonsoft.Json;
using sockx;



namespace ChatServer.Chat
{
    class Server
    {
        private List<User> connectedClients = new List<User>();
    

        IPHostEntry host;
        IPAddress IpAddr;
        IPEndPoint endPoint;
        Socket s_server;
        Socket s_cliente;
        public Server(string ip, int port) 
        {
            host = Dns.GetHostEntry(ip);
            IpAddr = host.AddressList[0];
            endPoint = new IPEndPoint(IpAddr, port);

            s_server = new Socket(IpAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            s_server.Bind(endPoint);
            s_server.Listen(10);
        }
        public void Start()
        {
            Thread t;
            while (true)
            {
                Console.WriteLine("Esperando conexiones");
                s_cliente = s_server.Accept();
                t = new Thread(start: clientConnection);
                t.Start(s_cliente);
                Console.WriteLine("Se ha conectado un cliente");
                

            }
         
        }
        public void clientConnection(object s)
        {
            Socket s_cliente = (Socket)s;
            byte[] Buffer;
            User user;
            string message;

            try
            {
                while (true)
                {
                    Buffer = new byte[1024];
                    s_cliente.Receive(Buffer);
                    user = (User)BinarySerialization.Deserializate(Buffer);
                    Console.WriteLine("Se recibio el mensaje de: " + user.name);
                    connectedClients.Add(user); 
                    SendUserListToAllClients(connectedClients);
                    Console.Out.Flush();
                    
                }
            }
            catch(SocketException se)
            {
              
                Console.WriteLine ("Se desconecto un cliente",se.Message);
               
            }
            
        }
        public string byte2string(byte[] Buffer)
        {
            string message;
            int endtext;

            message = Encoding.ASCII.GetString(Buffer);
            endtext = message.IndexOf("\0)");
            if (endtext > 0)
                message = message.Substring(0, endtext);

            return message;
        }
        private void SendUserListToAllClients(object tosend)
        {
            foreach (var item in connectedClients)
            {
                string serializedList = JsonConvert.SerializeObject(tosend);
                byte[] data = Encoding.UTF8.GetBytes(serializedList);
                s_cliente.Send(data);
                Console.WriteLine("Lista enviada");
            }
            
        }

    }
}
