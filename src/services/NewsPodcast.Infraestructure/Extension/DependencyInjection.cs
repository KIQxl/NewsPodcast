using Microsoft.Extensions.DependencyInjection;
using NewsPodcast.Core.Interfaces;
using NewsPodcast.Core.Services;
using NewsPodcast.Infraestructure.Services;

namespace NewsPodcast.Infraestructure.Extension
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructureServices(this IServiceCollection services)
        {
            services.AddTransient<INewsService, NewsService>();
            services.AddTransient<IOpenAIService, OpenAIService>();
            services.AddTransient<INoticeServices, NoticeServices>();
            services.AddTransient<IGoogleSpeechServices, GoogleSpeechService>();
            services.AddSingleton<HttpClient>();
            return services;
        }
    }
}
