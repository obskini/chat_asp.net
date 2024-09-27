using chat.Classes;
using chat.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace chat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IChatChannelApi _chatChannelApi;
        private readonly IChatMessageApi _chatMessageApi;

        public HomeController(ILogger<HomeController> logger, IChatChannelApi chatChannelApi, IChatMessageApi chatMessageApi)
        {
            _logger = logger;
            _chatChannelApi = chatChannelApi;
            _chatMessageApi = chatMessageApi;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Initial state, no user name or channels
            var model = new ChatViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string userName, string selectedChannel)
        {
            // Create the view model with the user's name
            var model = new ChatViewModel
            {
                UserName = userName
            };

            // Fetch available channels when user has entered the name
            if (!string.IsNullOrEmpty(userName))
            {
                model.ChatChannels = await _chatChannelApi.GetAllAsync();
            }

            // If a channel is selected, fetch messages for that channel
            if (!string.IsNullOrEmpty(selectedChannel))
            {
                model.SelectedChannel = selectedChannel;
                model.ChatMessages = await _chatMessageApi.GetMessagesAsync(selectedChannel);
            }

            // Return the updated view model to the view
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
