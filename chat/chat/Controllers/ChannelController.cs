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

        public ChannelController(IChatChannelApi chatChannelApi)
        {
            _chatChannelApi = chatChannelApi;
        }

        [HttpGet]
        public async Task<ActionResult<IList<ChatChannelResponse>>> GetChannels()
        {
            try
            {
                var channels = await _chatChannelApi.GetAllAsync();
                return Ok(channels);
            }
            catch (HttpRequestException e)
            {
                return BadRequest(new { message = $"Error retrieving channels: {e.Message}" });
            }
        }

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
                return CreatedAtAction(nameof(GetChannels), new { id = createdChannel.Id }, createdChannel); 
            }
            catch (HttpRequestException e)
            {
                return BadRequest(new { message = $"Error creating channel: {e.Message}" });
            }
        }
    }
}
