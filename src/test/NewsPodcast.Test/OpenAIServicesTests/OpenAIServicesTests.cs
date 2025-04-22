using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsPodcast.Core.Interfaces;
using NewsPodcast.Core.Models;
using NewsPodcast.Infraestructure.Extension;

namespace NewsPodcast.Test.OpenAIServicesTests
{
    [TestClass]
    public class OpenAIServicesTests
    {
        private IOpenAIService _openAIService;
        private List<Notice> notices = new List<Notice>
        {
            new Notice("noticia teste 1", "descrição teste", "conteudo teste", "www.teste.com.br", "image.test.url"),
            new Notice("noticia teste 2", "descrição teste", "conteudo teste", "www.teste.com.br", "image.test.url"),
            new Notice("noticia teste 3", "descrição teste", "conteudo teste", "www.teste.com.br", "image.test.url"),
            new Notice("noticia teste 4", "descrição teste", "conteudo teste", "www.teste.com.br", "image.test.url"),
            new Notice("noticia teste 5", "descrição teste", "conteudo teste", "www.teste.com.br", "image.test.url"),
            new Notice("noticia teste 6", "descrição teste", "conteudo teste", "www.teste.com.br", "image.test.url"),
            new Notice("noticia teste 7", "descrição teste", "conteudo teste", "www.teste.com.br", "image.test.url"),
            new Notice("noticia teste 8", "descrição teste", "conteudo teste", "www.teste.com.br", "image.test.url"),
            new Notice("noticia teste 9", "descrição teste", "conteudo teste", "www.teste.com.br", "image.test.url"),
            new Notice("noticia teste 10", "descrição teste", "conteudo teste", "www.teste.com.br", "image.test.url")
        };

        [TestInitialize]
        public void setup()
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

            _openAIService = provider.GetRequiredService<IOpenAIService>();
        }

        [TestMethod]
        public async Task SummaryNotices()
        {
            var summary = await _openAIService.SummaryNotices(notices);

            Assert.IsTrue(!string.IsNullOrEmpty(summary));
        }
    }
}
