using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using chat.Classes;
using chat.Models;

namespace chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController : ControllerBase
    {
        private readonly IChatChannelApi _chatChannelApi;

        // Injecteer de IChatChannelApi interface via de constructor
        public ChannelController(IChatChannelApi chatChannelApi)
        {
            _chatChannelApi = chatChannelApi;
        }

        // GET: api/channel
        // Haal alle chatkanalen op
        [HttpGet]
        public async Task<ActionResult<IList<ChatChannelResponse>>> GetChannels()
        {
            try
            {
                var channels = await _chatChannelApi.GetAllAsync();
                return Ok(channels); // Stuur de lijst van chatkanalen terug als JSON
            }
            catch (HttpRequestException e)
            {
                // Bij een fout geef een bad request terug
                return BadRequest(new { message = $"Error retrieving channels: {e.Message}" });
            }
        }

        // POST: api/channel
        // Maak een nieuw chatkanaal aan
        [HttpPost]
        public async Task<ActionResult<ChatChannelResponse>> CreateChannel([FromBody] ChatChannelResponse channel)
        {
            if (channel == null || string.IsNullOrEmpty(channel.Name))
            {
                return BadRequest(new { message = "Invalid channel data." });
            }

            try
            {
                var createdChannel = await _chatChannelApi.CreateAsync(channel);
                return CreatedAtAction(nameof(GetChannels), new { id = createdChannel.Id }, createdChannel); // Return 201 Created
            }
            catch (HttpRequestException e)
            {
                // Bij een fout geef een bad request terug
                return BadRequest(new { message = $"Error creating channel: {e.Message}" });
            }
        }
    }
}
