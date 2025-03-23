using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatProject.Models.DB;

namespace ChatProject.Models.DTO
{
    public enum ServerMessageType
    {
        Registration = 0,
        Login,
        History,
        Message,
        Info
    }

    public class ServerMessageBase
    {
        public ServerMessageType Type { get; set; }

        public ServerMessageBase(ServerMessageType type)
        {
            Type = type;
        }
    }

    public class ServerRegistrationMessage : ServerMessageBase
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public ServerRegistrationMessage()
            : base(ServerMessageType.Registration) { }
    }

    public class ServerLoginMessage : ServerMessageBase
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public ServerLoginMessage()
            : base(ServerMessageType.Login) { }
    }

    public class ServerInfoMessage : ServerMessageBase
    {
        public string Text { get; set; }
        public int TypeServerInfoMessage { get; set; } = 0;

        public ServerInfoMessage()
            : base(ServerMessageType.Info) { }
    }

    public class ServerHistoryMessage : ServerMessageBase
    {
        public List<MessageDTO> Messages { get; set; }

        public ServerHistoryMessage()
            : base(ServerMessageType.History) { }
    }

    public class ServerMessage : ServerMessageBase
    {
        public MessageDTO Message { get; set; }

        public ServerMessage()
            : base(ServerMessageType.Message) { }
    }
}