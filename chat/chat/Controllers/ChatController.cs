using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using chat.Classes;

namespace chat.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatChannelApi _chatChannelApi;

        public ChatController(IChatChannelApi chatChannelApi)
        {
            _chatChannelApi = chatChannelApi;
        }

        // Haal de kanalen op en geef ze weer in de sidebar
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var channels = await _chatChannelApi.GetAllAsync();
            return View(channels);
        }

        // Laad een specifiek kanaal wanneer een gebruiker erop klikt
        [HttpGet("chat/{id}")]
        public IActionResult Chat(int id)
        {
            // Voeg logica toe om specifieke chatberichten te laden voor het geselecteerde kanaal
            ViewBag.ChannelId = id;
            return View();
        }
    }
}
