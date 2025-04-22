using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsPodcast.Core.Interfaces;
using NewsPodcast.Infraestructure.Extension;

namespace NewsPodcast.Test.GoogleSpeechServiceTest
{
    [TestClass]
    public class GoogleSpeechTests
    {
        private IGoogleSpeechServices _speechService;

        [TestInitialize]
        public void Setup()
        {
            var services = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddInMemoryCollection() // Usando memória para configurações simples de teste
                .Build();

            services.AddSingleton<IConfiguration>(configuration); // Registrando a configuração
            services.AddInfraestructureServices();

            var provider = services.BuildServiceProvider();

            _speechService = provider.GetRequiredService<IGoogleSpeechServices>();
        }

        [TestMethod]
        public void TestSpeech()
        {
            _speechService.SpeechText("Deus é muito bom. Ele é a Razão da minha vida");

            Assert.IsTrue(true);
        }
    }
}
