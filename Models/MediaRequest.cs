namespace SttbApi.Models
{
    public class MediaRequest
    {
        public string Type { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public string? CoverImage { get; set; }
        public DateTime PublishedAt { get; set; }

        //  Article
        public string? Author { get; set; }
        public string? AuthorImage { get; set; }
        public string? AuthorDescription { get; set; }
        public string? Tagline { get; set; }
        public string? Content { get; set; }

        //  Video
        public string? YoutubeUrl { get; set; }
        public string? VideoTheme { get; set; }
        public string? VideoDescription { get; set; }

        //  Journal
        public string? FileUrl { get; set; }

        //  Bulletin
        public string? TitleDescription { get; set; }
        public string? Description { get; set; }

        //  Monograph
        public string? Image { get; set; }
        public string? DescriptionTitle { get; set; }
        public string? Writer { get; set; }
        public string? Synopsis { get; set; }
        public string? ISBN { get; set; }
    }
}