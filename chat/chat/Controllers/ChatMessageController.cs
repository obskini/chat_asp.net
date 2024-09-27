using chat.Classes;
using chat.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace chat.Controllers
{
    public class ChatMessageController : Controller
    {
        private readonly IChatMessageApi _chatMessageApi;
        private readonly IChatChannelApi _chatChannelApi;

        public ChatMessageController(IChatMessageApi chatMessageApi, IChatChannelApi chatChannelApi)
        {
            _chatMessageApi = chatMessageApi;
            _chatChannelApi = chatChannelApi;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string userName, string selectedChannel, string messageContent)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(selectedChannel) || string.IsNullOrEmpty(messageContent))
            {
                return RedirectToAction("Index", "Home", new { userName = userName });
            }

            // Create a new ChatMessage object to send
            var newMessage = new ChatMessage
            {
                Author = userName,
                Message = messageContent,
                Channel = selectedChannel,
                CreatedAt = System.DateTime.UtcNow
            };

            // Send the message using the ChatMessageApi
            await _chatMessageApi.SendMessageAsync(newMessage);

            // Fetch the updated messages for the channel after sending the new message
            var messages = await _chatMessageApi.GetMessagesAsync(selectedChannel);

            // Redirect back to the Home/Index page with the updated messages and state
            return RedirectToAction("Index", "Home", new { userName = userName, selectedChannel = selectedChannel });
        }
    }
}
