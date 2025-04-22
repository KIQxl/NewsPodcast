using Microsoft.Extensions.Configuration;
using NewsPodcast.Core.Extensions;
using NewsPodcast.Core.Interfaces;
using NewsPodcast.Core.Models;
using System.Text;
using System.Text.Json;

namespace NewsPodcast.Infraestructure.Services
{
    public class OpenAIService : IOpenAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apikey;

        public OpenAIService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apikey = config["OpenAI:ApiKey"];
            _httpClient.BaseAddress = new Uri("https://api.openai.com/");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apikey);
        }

        public async Task<string> SummaryNotices(List<Notice> notices)
        {
            if (notices is null || !notices.Any())
                return null;

            var formattedNotices = notices.FormatNotices();

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new []
                {
                    new { role = "developer", content = "Você é um assistente que resume notícias com uma linguagem acessível para podcasts, como se estivesse lendo para um ouvinte."},
                    new { role = "user", content = $"Resuma as seguintes noticias em um único texto, como se estivesse conversando com alguém, de forma bem natural e fluida: /n/n{formattedNotices}"}
                },
                temperature = 0.7
            };

            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("v1/chat/completions", content);

            if (!response.IsSuccessStatusCode)
                return string.Empty;

            var responseString = await response.Content.ReadAsStringAsync();

            using var jsonDoc = JsonDocument.Parse(responseString);
            var summary = jsonDoc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return summary;
        }
    }
}
