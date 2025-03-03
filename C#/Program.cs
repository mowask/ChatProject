using Chat.Model;
using Chat.Service;
using System.Text.Json;
using WebSocketSharp.Server;

namespace Chat
{
    public class Program
    {
        static void Main(string[] args)
        {
            var server = new WebSocketServer("ws://localhost:7890");
            server.AddWebSocketService<ChatClient>("/chat");

            server.Start();

            Console.WriteLine("Server has started");
            Console.ReadKey();
            server.Stop();
            Console.WriteLine("Server has stoped");
            Console.ReadKey();
        }
    }
}