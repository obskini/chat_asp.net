using chat.Classes;
using chat.Models;
using Microsoft.AspNetCore.Mvc;

namespace chat.Controllers
{
    public class ChatMessageController : Controller
    {
        private readonly IChatMessageApi _chatMessageApi;

        public ChatMessageController(IChatMessageApi chatMessageApi)
        {
            _chatMessageApi = chatMessageApi;
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string userName, string selectedChannel, string messageContent)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(selectedChannel) || string.IsNullOrEmpty(messageContent))
            {
                return RedirectToAction("Index", "Home", new { userName = userName });
            }

            var newMessage = new ChatMessage
            {
                Author = userName,
                Message = messageContent,
                Channel = selectedChannel,
                CreatedAt = System.DateTime.UtcNow
            };

            await _chatMessageApi.SendMessageAsync(newMessage);

            return RedirectToAction("Index", "Home", new { userName = userName, selectedChannel = selectedChannel });
        }
    }
}
