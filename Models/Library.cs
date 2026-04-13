using System.Text.Json.Serialization;

namespace SttbApi.Models
{
    public class Library
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;
        
        [JsonIgnore]
        public string? Slug { get; set; } = null!;

        public string Author { get; set; } = null!;
        public string Description { get; set; } = null!;

        public string Category { get; set; } = null!;
        public string Isbn { get; set; } = null!;

        public DateTime ReleaseDate { get; set; }

        public string CoverUrl { get; set; } = null!;
        public string? FileUrl { get; set; }

    }
}
