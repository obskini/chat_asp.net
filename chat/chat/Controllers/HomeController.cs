using chat.Classes;
using chat.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace chat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IChatChannelApi _chatChannelApi;
        private readonly IChatMessageApi _chatMessageApi;
        private const int PageSize = 10;

        public HomeController(ILogger<HomeController> logger, IChatChannelApi chatChannelApi, IChatMessageApi chatMessageApi)
        {
            _logger = logger;
            _chatChannelApi = chatChannelApi;
            _chatMessageApi = chatMessageApi;
        }

        [HttpGet, HttpPost]
        public async Task<IActionResult> Index(string userName, string selectedChannel, int page = 1)
        {
            var model = new ChatViewModel
            {
                UserName = userName,
                SelectedChannel = selectedChannel
            };

            if (!string.IsNullOrEmpty(userName))
            {
                var allChannels = await _chatChannelApi.GetAllAsync();
                int totalChannels = allChannels.Count;
                int totalPages = (int)Math.Ceiling(totalChannels / (double)PageSize);

                model.ChatChannels = allChannels
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize) 

                    .ToList();

                model.CurrentPage = page;
                model.TotalPages = totalPages;
            }


            if (!string.IsNullOrEmpty(selectedChannel))
            {
                model.ChatMessages = await _chatMessageApi.GetMessagesAsync(selectedChannel);
            }

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
