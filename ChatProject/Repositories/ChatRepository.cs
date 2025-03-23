using ChatProject.Contexts;
using ChatProject.Models.DB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Repositories
{
    public class ChatRepository
    {
        private ChatDbContext _context { get; set; }

        public ChatRepository(ChatDbContext context)
        {
            _context = context;
        }

        public bool NameExists(string userName)
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

        public User ValidateUser(string userName, string password)
        {
            return _context.Users
                    .Where(u => u.Name == userName 
                    && u.Password == password).FirstOrDefault();
        }

        public Message SaveMessage(string text, int userId)
        {
            var message = new Message()
            {
                Text = text,
                UserId = userId,
                Timestamp = DateTime.Now
            };

            _context.Messages.Add(message);
            _context.SaveChanges();

            return message;
        }

        public List<Message> GetMessageHistory()
        {
            return 
                _context.Messages
                .Include(m => m.User)
                .OrderByDescending(m => m.Timestamp)
                .Take(100).ToList();
        }
    }
}