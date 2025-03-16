using ChatNew.Models.DataBase;
using ChatNew.Models.DataTransferObject;
using System.Text.Json;
using static ChatNew.Models.DataTransferObject.ServerMessageBase;

namespace ChatNew.Servise
{
    public static class ResponseGen
    {
        public static string RegFailed()
        {
            var response = new ServerRegistrationMessage()
            {
                Success = false,
                ErrorMessage = "User doesn't exist"
            };

            return JsonSerializer.Serialize(response);            
        }


        public static string RegSuccess()
        {
            var response = new ServerRegistrationMessage()
            {
                Success = true               
            };

            return JsonSerializer.Serialize(response);
        }


        public static string LoginFailed()
        {
            var response = new ServerLoginMessage()
            {
                Success = false,
                ErrorMessage = "User doesn't exist"
            };

            return JsonSerializer.Serialize(response);
        }


        public static string LoginSuccess()
        {
            var response = new ServerLoginMessage()
            {
                Success = true
            };

            return JsonSerializer.Serialize(response);
        }


        public static string NewMessage(Messages message)
        {
            var response = new ServerMessage()
            {
                Message = new MessageDTO(message.Text, message.TimeStamp, message.User.Name)
            };

            return JsonSerializer.Serialize(response);
        }



        public static string InfoUserEntered(string userName)
        {
            var response = new ServerInfoMessage()
            {
                Text = $"{userName} has entered",
                //TypeServerInfoMessage = 1
            };
            return JsonSerializer.Serialize(response);
        }


        public static string InfoUserLeft(string userName)
        {
            var response = new ServerInfoMessage()
            {
                Text = $"{userName} has Left",
                //TypeServerInfoMessage = 2
            };
            return JsonSerializer.Serialize(response);
        }


        public static string GetHistory(List<Messages> messages)
        {
            var response = new ServerHistoryMessage()
            {
                Messages = messages.Select(m => new MessageDTO(m.Text, m.TimeStamp, m.User.Name)).ToList()
            };

            return JsonSerializer.Serialize(response);
        }
    }
}
