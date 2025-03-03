namespace Chat.Model
{
    public enum ChatMessageType
    {
        Server = 0,
        Connected = 1,
        User = 2
    }
    public class ChatMessage
    {
        public string Message { get; set; }
        public DateTime Time { get; set; }
        public ChatUser User { get; set; }

        public ChatMessageType Type { get; set; } = ChatMessageType.User;
    }
}