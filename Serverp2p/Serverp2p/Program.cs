using System; 
using LibreriaNodos;
using System.Threading;


namespace Serverp2p {

    public class Program {

        
        //atributos
        static int serverPuerto = 4405;
        static string serverIpAdress = "localHost";
        static string userServerName = "Server";
        static Thread Tlisten, Tbroadcast;

        static User serverUser = new User(userServerName, serverIpAdress, serverPuerto);

        static Server server = new Server(serverUser);

        

        public static void Main(string[] args)
        {

            Program program = new Program();
            server.start();
            Tlisten = new Thread(server.Listen);
            Tlisten.Start();
            
            Tbroadcast = new Thread(serverBroadcast);
            Tbroadcast.Start();
            



    

            



            
        }

        public static void serverBroadcast()
        {
            
            while (true)
            {
                try
                {
                    if (server.GetUsers() != null)
                    {

                        server.ServerBroadcastUserOnlineList();
                        Thread.Sleep(5000);

                    }
                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                
                
            }

        }

    }
}