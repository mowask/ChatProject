using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject.Models.DTO
{
    public class MessageDTO
    {
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserName { get; set; }

        public MessageDTO(string text, DateTime timestamp,
                            string userName)
        {
            Text = text;
            Timestamp = timestamp;
            UserName = userName;
        }
    }
}
