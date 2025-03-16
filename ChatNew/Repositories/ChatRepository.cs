using ChatNew.Contexts;
using ChatNew.Models.DataBase;
using Microsoft.EntityFrameworkCore;


namespace ChatNew.Repositories
{
    class ChatRepository
    {
        private ChatDbContext _context { get; set; }

        public ChatRepository (ChatDbContext context)
        {
            _context = context;
        }

        public bool NameExists (string userName)
        {
            return _context.Users.Any(u => u.Name == userName);
        }

        public User CreateUser(string userName, string password)
        {
            var user = new User()
            {
                Name = userName,
                Password = password
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User ValidateUser (string userName, string password)
        {
            return _context.Users.Where(u => u.Name == userName && u.Password == password).FirstOrDefault();
        }

        public Messages SaveMessage(string text, int userId)
        {
            var message = new Messages()
            {
                Text = text,
                UserId = userId,
                TimeStamp = DateTime.Now
            };

            _context.Messages.Add(message);
            _context.SaveChanges();

            return message;
        }


        public List<Messages> GetMessageHistory()
        {
            return  _context.Messages.Include(m => m.User).OrderByDescending(m => m.TimeStamp).Take(100).ToList();
        }
    }
}
