namespace ChatNew.Models.DataTransferObject
{
    class ClientMessages
    {
        public enum ClientMessageType
        {
            Registration = 0,
            Login = 1,
            History = 2, 
            Message = 3
        }


        public class ClientMessageBase
        {
            public ClientMessageType Type { get; set; }
            public ClientMessageBase (ClientMessageType type)
            {
                Type = type;
            }
        }


        public class ClientRegistrationMessage : ClientMessageBase
        {
            public string Name { get; set; }
            public string Password { get; set; }

            public ClientRegistrationMessage() : base(ClientMessageType.Registration) { }
        }


        public class ClientLoginMessage : ClientMessageBase
        {
            public string Name { get; set; }
            public string Password { get; set; }

            public ClientLoginMessage() : base(ClientMessageType.Login) { }
        }


        public class ClientMessage : ClientMessageBase
        {
            public string Text { get; set; }            

            public ClientMessage () : base(ClientMessageType.Message) { }
        }
    }
}
