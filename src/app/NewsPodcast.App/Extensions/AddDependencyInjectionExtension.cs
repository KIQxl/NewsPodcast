using Microsoft.Extensions.DependencyInjection;
using NewsPodcast.App.Pages;

namespace NewsPodcast.App.Extensions
{
    public static class AddDependencyInjectionExtension
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            services.AddTransient<MainWindow>();
            services.AddTransient<HomePage>();
            services.AddTransient<CategoryNotice>();

            return services;
        }
    }
}
