namespace SttbApi.Models
{
    public class Article
    {
        public int Id { get; set; }
        public int MediaId { get; set; }
        public Media Media { get; set; }

        public string Author { get; set; }
        public string AuthorImage { get; set; }
        public string AuthorDescription { get; set; }

        public string Tagline { get; set; }
        public string Content { get; set; }
    }
}
