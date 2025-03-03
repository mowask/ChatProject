using Chat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Chat.Service
{
    public static class ServiceMessageGenerator
    {
        public static string GenOpened(string id)
        {
            return MessageJSONService.Serialize
                    (
                        $"You are connected. Your ID is {id}. Enter your name: ",
                        null,
                        ChatMessageType.Server
                    );
        }

        public static string GenConnected(ChatUser user)
        {
            return MessageJSONService.Serialize
                    (
                        "",
                        user,
                        ChatMessageType.Connected
                    );
        }

        public static string GenEntered(string name)
        {
            return MessageJSONService.Serialize
                    (
                        $"{name} entered the chat",
                        null,
                        ChatMessageType.Server
                    );
        }

        public static string GenLeft(string name)
        {
            return MessageJSONService.Serialize
            (
                        $"{name}: left the chat",
                        null,
                        ChatMessageType.Server
                    );
        }
    }
}