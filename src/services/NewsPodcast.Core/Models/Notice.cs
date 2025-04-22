using System.Text;

namespace NewsPodcast.Core.Models
{
    public class Notice
    {
        public Notice() { }

        public Notice(string title, string description, string content, string noticeURL, string imageURL)
        {
            Title = title;
            Description = description;
            Content = content;
            NoticeURL = noticeURL;
            ImageURL = imageURL;
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string NoticeURL { get; set; }
        public string ImageURL { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"Título: {Title}");
            sb.AppendLine($"Descrição: {Description}");
            sb.AppendLine($"Conteúdo: {Content}");
            sb.AppendLine();

            return sb.ToString();
        }
    }
}