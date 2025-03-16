namespace ChatNew.Models.DataTransferObject
{
    public class MessageDTO
    {
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
        public string UserName { get; set; }


        public MessageDTO(string text, DateTime timeStamp, string userName)
        {
            Text = text;
            TimeStamp = timeStamp;
            UserName = userName;
        }
    }
}
