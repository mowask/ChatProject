using ChatProject.Models.DB;
using ChatProject.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatProject.Services
{
    public static class ResponseGen
    {
        public static string RegFailed()
        {
            var response = new ServerRegistrationMessage()
            {
                IsSuccess = false,
                ErrorMessage = "User doesn't exist"
            };

            return JsonSerializer.Serialize(response);
        }

        public static string RegSuccess()
        {
            var response = new ServerRegistrationMessage()
            {
                IsSuccess = true
            };

            return JsonSerializer.Serialize(response);
        }

        public static string InfoUserEntered(string userName)
        {
            var response = new ServerInfoMessage()
            {
                Text = $"{userName} has entered.",
                TypeServerInfoMessage = 1
            };
            return JsonSerializer.Serialize(response);
        }

        public static string InfoUserLeft(string userName)
        {
            var response = new ServerInfoMessage()
            {
                Text = $"{userName} has left.",
                TypeServerInfoMessage = 2
            };
            return JsonSerializer.Serialize(response);
        }

        public static string GetHistory(List<Message> messages)
        {
            var response = new ServerHistoryMessage()
            {
                Messages = messages
                           .Select(m => new MessageDTO
                           (
                               m.Text, m.Timestamp, m.User.Name
                           )).ToList()
            };

            return JsonSerializer.Serialize(response);
        }

        public static string LoginFailed()
        {
            var response = new ServerLoginMessage()
            {
                IsSuccess = false,
                ErrorMessage = "Login failed"
            };

            return JsonSerializer.Serialize(response);
        }

        public static string LoginSuccess()
        {
            var response = new ServerLoginMessage()
            {
                IsSuccess = true
            };

            return JsonSerializer.Serialize(response);
        }

        public static string NewMessage(Message message)
        {
            var response = new ServerMessage()
            {
                Message = new MessageDTO(message.Text,
                                        message.Timestamp,
                                        message.User.Name)
            };

            return JsonSerializer.Serialize(response);
        }
    }
}