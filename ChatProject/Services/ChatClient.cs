using System;
using WebSocketSharp.Server;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatProject.Models.DB;
using ChatProject.Models.DTO;
using ChatProject.Contexts;
using WebSocketSharp;
using System.Text.Json;
using ChatProject.Repositories;


namespace ChatProject.Services
{
    public class ChatClient : WebSocketBehavior
    {
        // По умолчанию ↓
        // string id
        // Sessions
        // Send()
        private ChatRepository _repository { get; set; }
        public User User { get; set; } = null;

        public ChatClient(ChatRepository repository)
        {
            _repository = repository;
        }

        protected override void OnOpen()
        {
            Console.WriteLine($"User {ID} connected to the chat");
        }

        protected override void OnClose(CloseEventArgs e)
        {
            Console.WriteLine($"User {ID} left the chat");
            if (User == null) return;
            Sessions.Broadcast(ResponseGen.InfoUserLeft(User.Name));
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            string request = e.Data;

            var baseMessage = JsonSerializer.Deserialize<ClientMessageBase>(request);

            switch(baseMessage.Type)
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

        private void HandleMessage(string request)
        {
            if (User == null) return;
            var message = JsonSerializer.Deserialize<ClientMessage>(request);
            var messageObj = _repository.SaveMessage(message.Text, User.Id);
            Sessions.Broadcast(ResponseGen.NewMessage(messageObj));
        }

        public void HandleRegistration(string request)
        {
            var message = JsonSerializer.Deserialize<ClientRegistrationMessage>(request);

            if (_repository.NameExists(message.Name))
            {
                Send(ResponseGen.RegFailed());
                return;
            }
            
            User = _repository
                .CreateUser(message.Name, message.Password);

            Send(ResponseGen.RegSuccess());

            var messages = _repository.GetMessageHistory();
            Send(ResponseGen.GetHistory(messages));

            BroadcastAllNotSelf(ResponseGen.InfoUserEntered(message.Name));
        }

        public void BroadcastAllNotSelf(string message)
        {
            foreach (var session in Sessions.Sessions)
            {
                if (session.ID == ID) continue;
                session.Context.WebSocket.Send(message);
            }
        }

        public void HandleLogin(string request)
        {
            var message = JsonSerializer
                .Deserialize<ClientLoginMessage>(request);
            User = _repository
                .ValidateUser(message.Name, message.Password);

            if(User == null)
            {
                Send(ResponseGen.LoginFailed());
                return;
            }
            Send(ResponseGen.LoginSuccess());

            var messages = _repository.GetMessageHistory();
            Send(ResponseGen.GetHistory(messages));

            BroadcastAllNotSelf(ResponseGen.InfoUserEntered(message.Name));
        }
    }
}