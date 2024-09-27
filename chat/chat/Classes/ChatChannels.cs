using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
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

        // Haal alle chatkanalen op via een GET request
        public async Task<IList<ChatChannelResponse>> GetAllAsync()
        {
            var httpClient = _httpClientFactory.CreateClient("TalkApi");
            var route = "/api/chat-channels"; // De endpoint voor het ophalen van de kanalen

            var result = await httpClient.GetAsync(route);
            result.EnsureSuccessStatusCode(); // Controleer of de response succesvol is

            // Map de response naar een lijst van ChatChannelResponse
            return await result.Content.ReadAsAsync<IList<ChatChannelResponse>>();
        }

        // Creëer een nieuw chatkanaal via een POST request
        public async Task<ChatChannelResponse> CreateAsync(ChatChannelResponse request)
        {
            var httpClient = _httpClientFactory.CreateClient("TalkApi");
            var route = "/api/chat-channels"; // De endpoint voor het creëren van een kanaal

            // Verstuur het POST verzoek en stuur het request body mee
            var result = await httpClient.PostAsJsonAsync(route, request);
            result.EnsureSuccessStatusCode(); // Controleer of de response succesvol is

            // Map de response naar een ChatChannelResponse object
            return await result.Content.ReadAsAsync<ChatChannelResponse>();
        }
    }
}
