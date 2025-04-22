using NewsPodcast.Core.Models;

namespace NewsPodcast.Core.Extensions
{
    public static class NoticeExtensions
    {
        public static string FormatNotices(this List<Notice> notices)
        {
            return string.Join(Environment.NewLine, notices.Select(x => x.ToString()));
        }
    }
}
