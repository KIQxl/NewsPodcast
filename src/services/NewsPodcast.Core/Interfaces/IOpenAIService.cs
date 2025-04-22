using NewsPodcast.Core.Models;

namespace NewsPodcast.Core.Interfaces
{
    public interface IOpenAIService
    {
        public Task<string> SummaryNotices(List<Notice> notices);
    }
}
