namespace NewsPodcast.Core.Models
{
    public class ResponseNotice
    {
        public ResponseNotice(List<Notice>? notices, string? summaryNotices, byte[]? noticesDataAudio)
        {
            Notices = notices;
            SummaryNotices = summaryNotices;
            NoticesDataAudio = noticesDataAudio;
            IsSuccess = true;
        }

        public ResponseNotice(bool isSuccess)
        {
            Notices = null;
            SummaryNotices = null;
            NoticesDataAudio = null;
            IsSuccess = isSuccess;
        }

        public List<Notice>? Notices { get; set; } = new List<Notice>();
        public string? SummaryNotices { get; set; }
        public byte[]? NoticesDataAudio { get; set; }
        public bool IsSuccess { get; set; }
    }
}
