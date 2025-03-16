
namespace ChatNew.Models.DataBase
{
    public class Messages
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
