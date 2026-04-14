using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SttbApi.Models
{
    public class Media
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Slug { get; set; }
        public string Type { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public string CoverImage { get; set; }
        public DateTime PublishedAt { get; set; }

        // Navigation
        public Article Article { get; set; }
        public Video Video { get; set; }
        public Journal Journal { get; set; }
        public Bulletin Bulletin { get; set; }
        public Monograph Monograph { get; set; }
    }
}
