using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Models.DTO
{
    public enum ClientMessageType
    {
        Registration = 0,
        Login,
        History,
        Message
    }

    public class ClientMessageBase
    {
        public ClientMessageType Type { get; set; }

        public ClientMessageBase(ClientMessageType type)
        {
            Type = type;
        }
    }

    public class ClientRegistrationMessage : ClientMessageBase
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public ClientRegistrationMessage()
            : base(ClientMessageType.Registration) { }
    }

    public class ClientLoginMessage : ClientMessageBase
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public ClientLoginMessage()
            : base(ClientMessageType.Login) { }
    }

    public class ClientMessage : ClientMessageBase
    {
        public string Text { get; set; }
        public ClientMessage()
            : base(ClientMessageType.Message) { }
    }
}