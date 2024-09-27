using System;

namespace chat.Models
{
    public class ChatChannelResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } // Zorg ervoor dat de datum correct wordt gemapt
    }
}
