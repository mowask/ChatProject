namespace ChatNew.Models.DataTransferObject
{
    public enum ServerMessagesType
    {
        Registration = 0,
        Login = 1,
        History = 2,
        Message = 3,
        Info = 4
    }
    public class ServerMessageBase 
    {
        public ServerMessagesType Type { get; set; }

        public ServerMessageBase (ServerMessagesType type)
        {
            Type = type;
        }


        public class ServerRegistrationMessage : ServerMessageBase
        {
            public bool Success { get; set; }
            public string ErrorMessage { get; set; }

            public ServerRegistrationMessage() : base(ServerMessagesType.Registration) { }            
        }


        public class ServerLoginMessage : ServerMessageBase
        {
            public bool Success { get; set; }
            public string ErrorMessage { get; set; }

            public ServerLoginMessage() : base(ServerMessagesType.Login) { }
        }


        public class ServerInfoMessage : ServerMessageBase
        {
            public string Text { get; set; }
            //public int TypeServerInfoMessage { get; set; } = 0;

            public ServerInfoMessage() : base(ServerMessagesType.Info) { }
        }


        public class ServerHistoryMessage : ServerMessageBase
        {
            public List<MessageDTO> Messages { get; set; }

            public ServerHistoryMessage() : base(ServerMessagesType.History) { }
        }


        public class ServerMessage : ServerMessageBase
        {
            public MessageDTO Message {get; set;}
            public ServerMessage() : base(ServerMessagesType.Message) { }
        }
    }
}
