using chat.Models;

namespace chat.Classes
{
    public interface IChatChannelApi
    {
        Task<IList<ChatChannelResponse>> GetAllAsync();
        Task<ChatChannelResponse> CreateAsync(ChatChannelResponse request);
    }

    public class ChatChannelApi : IChatChannelApi
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ChatChannelApi(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<ChatChannelResponse>> GetAllAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("TalkApi");
            var route = "/api/chat-channels"; 

            var result = await httpClient.GetAsync(route);
            result.EnsureSuccessStatusCode();

            return await result.Content.ReadAsAsync<IList<ChatChannelResponse>>();
        }

        public async Task<ChatChannelResponse> CreateAsync(ChatChannelResponse request)
        {
            var httpClient = _httpClientFactory.CreateClient("TalkApi");
            var route = "/api/chat-channels";

            var result = await httpClient.PostAsJsonAsync(route, request);
            result.EnsureSuccessStatusCode();
            return await result.Content.ReadAsAsync<ChatChannelResponse>();
        }
    }
}
