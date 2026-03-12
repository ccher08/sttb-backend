using System.Text.Json.Serialization;

namespace SttbApi.Models
{
    public class OverviewAbout
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int ProgramStudiId { get; set; }

        [JsonIgnore]
        public ProgramStudi? ProgramStudi { get; set; }
    }
}
