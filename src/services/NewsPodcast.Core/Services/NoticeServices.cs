using NewsPodcast.Core.Interfaces;
using NewsPodcast.Core.Models;

namespace NewsPodcast.Core.Services
{
    public class NoticeServices : INoticeServices
    {
        private readonly INewsService _newsServices;
        private readonly IOpenAIService _openAIService;
        private readonly IGoogleSpeechServices _googleSpeechServices;

        public NoticeServices(INewsService newsServices, IOpenAIService openAIService, IGoogleSpeechServices googleSpeechServices)
        {
            _newsServices = newsServices;
            _openAIService = openAIService;
            _googleSpeechServices = googleSpeechServices;
        }

        public async Task<ResponseNotice> HandleNoticeByCategory(string category)
        {
            // Collect notices by GNews API
            var notices = await _newsServices.GetNoticesByCategory(category);
            if (notices is null)
                return new ResponseNotice(false);

            // Send notices for OPENAI to resume
            var summaryNotices = await _openAIService.SummaryNotices(notices);
            if (summaryNotices == null)
                return new ResponseNotice(false);

            // Convert resume notices by OPENAI in audio
            var byteAudio = _googleSpeechServices.SpeechText(summaryNotices);
            if(byteAudio == null) 
                return new ResponseNotice(false);

            return new ResponseNotice(notices, summaryNotices, byteAudio);
        }

        public async Task<ResponseNotice> HandleNoticeAboutBy(string about)
        {
            // Collect notices by GNews API
            var notices = await _newsServices.GetNoticesAboutBy(about);
            if (notices is null)
                return new ResponseNotice(false);

            // Send notices for OPENAI to resume
            var summaryNotices = await _openAIService.SummaryNotices(notices);
            if (summaryNotices == null)
                return new ResponseNotice(false);

            // Convert resume notices by OPENAI in audio
            var byteAudio = _googleSpeechServices.SpeechText(summaryNotices);
            if (byteAudio == null)
                return new ResponseNotice(false);

            return new ResponseNotice(notices, summaryNotices, byteAudio);
        }
    }
}
