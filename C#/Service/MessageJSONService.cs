using Chat.Model;
using System.Text.Json;
using System.Xml.Linq;

namespace Chat.Service
{
    public static class MessageJSONService
    {
        public static string Serialize(string textMessage,
                                              ChatUser user,
                                              ChatMessageType type = ChatMessageType.User)
        {
            var message = new ChatMessage()
            {
                Message = textMessage,
                Time = DateTime.Now,
                User = user,
                Type = type
            };

            return JsonSerializer.Serialize(message);
        }

        public static string Serialize(ChatMessage message)
        {
            return JsonSerializer.Serialize(message);
        }

        public static ChatMessage Deserialize(string message)
        {
            try
            {
                return JsonSerializer.Deserialize<ChatMessage>(message);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new ChatMessage();
        }
    }
}