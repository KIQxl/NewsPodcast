using Microsoft.Extensions.DependencyInjection;
using NewsPodcast.Core.Interfaces;
using NewsPodcast.Infraestructure.Extension;

namespace NewsPodcast.Test.NoticeServicesTests
{
    [TestClass]
    public class NoticeServicesTests
    {
        private INoticeServices _services;

        [TestInitialize]
        public void setup()
        {
            var services = new ServiceCollection();
            services.AddInfraestructureServices();

            var provider = services.BuildServiceProvider();

            _services = provider.GetRequiredService<INoticeServices>();
        }

        [TestMethod]
        public async Task HandleByCategory()
        {
            var bytesAudio = await _services.HandleNoticeByCategory("world");

            Assert.IsNotNull(bytesAudio);
        }

        [TestMethod]
        public async Task HandleAboutBy()
        {
            var bytesAudio = await _services.HandleNoticeAboutBy("moda");

            Assert.IsNotNull(bytesAudio);
        }
    }
}
