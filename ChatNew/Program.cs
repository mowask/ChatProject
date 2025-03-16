using ChatNew.Contexts;
using ChatNew.Repositories;
using ChatNew.Servise;
using WebSocketSharp.Server;

namespace ChatNew
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ChatDbContext())
            {

                var repository = new ChatRepository(context);

                var server = new WebSocketServer("ws://localhost:7890");
                server.AddWebSocketService("/chat", () => new ChatClient(repository));

                server.Start();

                Console.WriteLine("Server has started");
                Console.ReadKey();
                server.Stop();
                Console.WriteLine("Server has stopped");
                Console.ReadKey();            
            };           

        }
    }
}