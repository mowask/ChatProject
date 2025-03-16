using ChatNew.Models.DataBase;
using ChatNew.Repositories;
using System.Text.Json;
using WebSocketSharp;
using WebSocketSharp.Server;
using static ChatNew.Models.DataTransferObject.ClientMessages;


namespace ChatNew.Servise
{
    class ChatClient : WebSocketBehavior
    {
        // Id
        // Sand()
        // Sassions


        private ChatRepository _repository { get; set; }
        public User User { get; set; } = null;

        public ChatClient (ChatRepository repository)
        {
            _repository = repository;           
        }


        protected override void OnOpen()
        {
            Console.WriteLine($"User {ID} connected.");
        }

        protected override void OnClose(CloseEventArgs e)
        {
            Console.WriteLine($"User {ID} disconnected.");
            //if (User == null) return;
            Sessions.Broadcast(ResponseGen.InfoUserLeft(User.Name));
        }


        protected override void OnMessage(MessageEventArgs e)
        {
            string request = e.Data;

            var baseMessage = JsonSerializer.Deserialize<ClientMessageBase>(request);

            switch (baseMessage.Type)
            {
                case ClientMessageType.Registration:
                    HandleRegistration(request);
                    break;
                case ClientMessageType.Login:
                    HandleLogin(request);
                    break;
                case ClientMessageType.Message:
                    HandleMessage(request);
                    break;
                default:
                    Console.WriteLine(request);
                    break;
            }
        }
       

        public void HandleRegistration (string request)
        {
            var message = JsonSerializer.Deserialize<ClientRegistrationMessage>(request);

            if (_repository.NameExists(message.Name))
            {               
                Send(ResponseGen.RegFailed());
                return;
            }
                        
            User = _repository.CreateUser(message.Name, message.Password);

            Send(ResponseGen.RegSuccess());

            var messages = _repository.GetMessageHistory();
            Send(ResponseGen.GetHistory(messages));

            BroadcastAllNotSelf(ResponseGen.InfoUserEntered(message.Name));
        }


        public void HandleLogin(string request)
        {
            var message = JsonSerializer.Deserialize<ClientLoginMessage>(request);
            User = _repository.ValidateUser(message.Name, message.Password);

            if (User == null)
            {
                Send(ResponseGen.LoginFailed());
                return ;
            }

            Send(ResponseGen.LoginSuccess());

            var messages = _repository.GetMessageHistory();
            Send(ResponseGen.GetHistory(messages));

            BroadcastAllNotSelf(ResponseGen.InfoUserEntered(message.Name));
        }

        public void HandleMessage(string request)
        {           
            if (User == null)           
                return;

            var message = JsonSerializer.Deserialize<ClientMessage>(request);
            var messageObj = _repository.SaveMessage(message.Text, User.Id);
            Sessions.Broadcast(ResponseGen.NewMessage(messageObj));
 
        }

        public void BroadcastAllNotSelf (string message)
        {
            foreach(var otherID in Sessions.IDs)
            {
                if (otherID == ID) continue;
               Sessions.SendTo(message, otherID);
            }
        }




        //public void BroadcastAllNotSelf(string message)
        //{
        //    foreach (var session in Sessions.Sessions)
        //    {
        //        if (session.ID == ID) continue;
        //        session.Context.WebSocket.Send(message);
        //    }
        //}


    }
}
