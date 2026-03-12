using System.Text.Json.Serialization;

namespace SttbApi.Models
{
    public class CompetencyGroup
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int ProgramStudiId { get; set; }

        [JsonIgnore]
        public ProgramStudi? ProgramStudi { get; set; }

        public List<CompetencyItem>? Items { get; set; }
    }
}
