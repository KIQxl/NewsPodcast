using NewsPodcast.Core.Models;
using NewsPodcast.Infraestructure.Models;

namespace NewsPodcast.Infraestructure.Extension
{
    public static class NewsAPIResponseObjectExtensions
    {
        public static List<Notice> ToNotice(this NewsAPIResponseObject response)
        {
            var notices = response.Articles
                .Select(x => new Notice { 
                    Title = x.Title, 
                    Description = x.Description, 
                    Content = x.Content, 
                    ImageURL = x.Image, 
                    NoticeURL = x.Url}
                ).ToList();

            return notices;
        }
    }
}
