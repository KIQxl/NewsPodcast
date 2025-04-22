using NewsPodcast.Core.Models;

namespace NewsPodcast.Core.Interfaces
{
    public interface INewsService
    {
        public Task<List<Notice>> GetNoticesByCategory(string category);
        public Task<List<Notice>> GetNoticesAboutBy(string about);
    }
}
