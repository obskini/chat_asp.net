using chat.Classes;
using chat.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace chat.Controllers
{
    public class ChannelController : Controller
    {
        private readonly IChatChannelApi _chatChannelApi;

        public ChannelController(IChatChannelApi chatChannelApi)
        {
            _chatChannelApi = chatChannelApi;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string channelName, string userName)
        {
            if (string.IsNullOrEmpty(channelName))
            {
                return RedirectToAction("Index", "Home", new { userName = userName });
            }

            var newChannel = new ChatChannelResponse
            {
                Name = channelName
            };

            await _chatChannelApi.CreateAsync(newChannel);
            return RedirectToAction("Index", "Home", new { userName = userName });
        }
    }
}
