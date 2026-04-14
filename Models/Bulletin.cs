namespace SttbApi.Models
{
    public class Bulletin
    {
        public int Id { get; set; }
        public int MediaId { get; set; }
        public Media Media { get; set; }

        public string Author { get; set; }
        public string PdfUrl { get; set; }

        public string TitleDescription { get; set; }
        public string Description { get; set; }
    }
}
