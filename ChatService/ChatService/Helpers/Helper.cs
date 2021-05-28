using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatService.Helpers
{
    public class Helper
    {
        public static List<Message> Messages { get; set; } = new List<Message>();
        public static Message LastMessageSent { get; set; }

        static Helper()
        {
            Messages.Add(new Message() { ClientName = "", Content = "" });
            LastMessageSent = Messages.Last();
        }
    }
}
