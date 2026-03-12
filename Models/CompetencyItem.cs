using System.Text.Json.Serialization;

namespace SttbApi.Models
{
    public class CompetencyItem
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int CompetencyGroupId { get; set; }

        [JsonIgnore]
        public CompetencyGroup? CompetencyGroup { get; set; }
    }
}
