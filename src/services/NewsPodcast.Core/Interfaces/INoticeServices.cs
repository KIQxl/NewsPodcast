using NewsPodcast.Core.Models;

namespace NewsPodcast.Core.Interfaces
{
    public interface INoticeServices
    {
        public Task<ResponseNotice> HandleNoticeByCategory(string category);

        public Task<ResponseNotice> HandleNoticeAboutBy(string about);
    }
}
