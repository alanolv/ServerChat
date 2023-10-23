using ChatServer.Chat;
using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
         Server s =new Server("localhost",4404);
         s.Start();

        }
    }
}