using ChatProject.Contexts;
using ChatProject.Models.DB;
using ChatProject.Repositories;
using ChatProject.Services;
using WebSocketSharp.Server;

namespace ChatProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var context = new ChatDbContext())
            {
                var repository = new ChatRepository(context);
                var server = new WebSocketServer
                            ("ws://localhost:7890");
                server.AddWebSocketService<ChatClient>
                        ("/chat", () => 
                        new ChatClient(repository));

                server.Start();

                Console.WriteLine("Server has started");
                Console.ReadKey();
                server.Stop();
                Console.WriteLine("Server has stoped");
                Console.ReadKey();
            }

            
        }
    }
}