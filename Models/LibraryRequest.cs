namespace SttbApi.Models
{
    public class LibraryRequest
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Isbn { get; set; }
        public DateTime ReleaseDate { get; set; }

        public IFormFile Cover { get; set; }
        public IFormFile File { get; set; }
    }
}
