namespace chat.Models
{
    public class ChatViewModel
    {
        public string UserName { get; set; }
        public IList<ChatChannelResponse> ChatChannels { get; set; }
        public IList<ChatMessage> ChatMessages { get; set; }
        public string SelectedChannel { get; set; }
    }
}
