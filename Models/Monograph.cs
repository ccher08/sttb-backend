namespace SttbApi.Models
{
    public class Monograph
    {
        public int Id { get; set; }
        public int MediaId { get; set; }
        public Media Media { get; set; }

        public string Author { get; set; }
        public string Image { get; set; }

        public string DescriptionTitle { get; set; }
        public string Writer { get; set; }
        public string Synopsis { get; set; }

        public string ISBN { get; set; }
    }
}
