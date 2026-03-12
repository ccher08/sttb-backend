using System;
using System.ComponentModel.DataAnnotations;

namespace SttbApi.Models
{
    public class Berita
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Content { get; set; }

        [Required]
        public string Category { get; set; }

        public DateTime? Date { get; set; }

        public string Time { get; set; }

        public string ImageUrl { get; set; }

        public bool IsFeatured { get; set; } = false;
    }
}
