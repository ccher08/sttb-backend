namespace SttbApi.Models
{
    public class Journal
    {
        public int Id { get; set; }
        public int MediaId { get; set; }
        public Media Media { get; set; }

        public string FileUrl { get; set; }
    }
}
