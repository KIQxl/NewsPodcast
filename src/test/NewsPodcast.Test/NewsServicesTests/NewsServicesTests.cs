using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsPodcast.Core.Interfaces;
using NewsPodcast.Infraestructure.Extension;

namespace NewsPodcast.Test.NewsServicesTests
{
    [TestClass]
    public class NewsServicesTests
    {
        private INewsService _newsServices;

        [TestInitialize]
        public void Setup()
        {
            var services = new ServiceCollection();

            // Simulando a configuração para os testes
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddInMemoryCollection() // Usando memória para configurações simples de teste
                .Build();

            services.AddSingleton<IConfiguration>(configuration); // Registrando a configuração

            services.AddInfraestructureServices();
            var provider = services.BuildServiceProvider();
            _newsServices = provider.GetRequiredService<INewsService>();
        }

        [TestMethod]
        public async Task RequestAPIandReturnNoticeListByCategory()
        {
            var notices = await _newsServices.GetNoticesByCategory("sports");

            Assert.IsNotNull(notices);
        }

        [TestMethod]
        public async Task RequestAPIandReturnNoticeListAboutBy()
        {
            var test = Environment.GetEnvironmentVariable("GNews:ApiKey");
            var notices = await _newsServices.GetNoticesAboutBy("bolsonaro");

            Assert.IsNotNull(notices);
        }
    }
}
