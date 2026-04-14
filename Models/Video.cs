namespace SttbApi.Models
{
    public class Video
    {
        public int Id { get; set; }
        public int MediaId { get; set; }
        public Media Media { get; set; }

        public string YoutubeUrl { get; set; }
        public string Theme { get; set; }
        public string Description { get; set; }
    }
}
