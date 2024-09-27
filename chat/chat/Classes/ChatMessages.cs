using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using chat.Models;

namespace chat.Classes
{
    public interface IChatMessageApi
    {
        Task<IList<ChatMessage>> GetMessagesAsync(string channelName);
        Task SendMessageAsync(ChatMessage message); // Add this line
    }

    public class ChatMessageApi : IChatMessageApi
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ChatMessageApi(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<ChatMessage>> GetMessagesAsync(string channelName)
        {
            var httpClient = _httpClientFactory.CreateClient("TalkApi");
            var route = $"/api/chat-messages?channel={channelName}"; // Fetch messages for the specific channel

            var result = await httpClient.GetAsync(route);
            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsAsync<IList<ChatMessage>>(); // Deserialize to a list of ChatMessage
        }

        public async Task SendMessageAsync(ChatMessage message)
        {
            var httpClient = _httpClientFactory.CreateClient("TalkApi");
            var route = "/api/chat-messages"; // POST endpoint

            var result = await httpClient.PostAsJsonAsync(route, message);
            result.EnsureSuccessStatusCode();
        }
    }
}
