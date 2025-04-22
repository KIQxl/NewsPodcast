using Microsoft.Extensions.Configuration;
using NewsPodcast.Core.Interfaces;
using NewsPodcast.Core.Models;
using NewsPodcast.Infraestructure.Extension;
using NewsPodcast.Infraestructure.Models;
using System.Text.Json;

namespace NewsPodcast.Infraestructure.Services
{
    public class NewsService : INewsService
    {
        private readonly HttpClient _httpClient;
        private string _urlGetByCategory = "https://gnews.io/api/v4/top-headlines?category=";
        private string _urlGetAbout = "https://gnews.io/api/v4/search?q=";
        private string _apiKey;
        private string _langParameter = "pt";
        private int _maxReturn = 12;

        public NewsService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["GNews:ApiKey"];
        }

        public async Task<List<Notice>> GetNoticesAboutBy(string about)
        {
            string url = $"{_urlGetAbout}{about}&lang={_langParameter}&max={_maxReturn}&apikey={_apiKey}";
            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response != null && response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    NewsAPIResponseObject responseObject = JsonSerializer.Deserialize<NewsAPIResponseObject>(await response.Content.ReadAsStringAsync(), options);
                    var notices = responseObject.ToNotice();

                    if(!notices.Any() || notices is null)
                        return null;

                    return notices;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Notice>> GetNoticesByCategory(string category)
        {
            string url = $"{_urlGetByCategory}{category}&lang={_langParameter}&max={_maxReturn}&apikey={_apiKey}";
            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response != null && response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    NewsAPIResponseObject responseObject = JsonSerializer.Deserialize<NewsAPIResponseObject>(await response.Content.ReadAsStringAsync(), options);
                    var notices = responseObject.ToNotice();

                    if (!notices.Any() || notices is null)
                        return null;

                    return notices;
                }

                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
