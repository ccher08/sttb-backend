using System.Text.Json.Serialization;

namespace SttbApi.Models
{
    public class CurriculumGroup
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public int ProgramStudiId { get; set; }

        [JsonIgnore]
        public ProgramStudi? ProgramStudi { get; set; }

        public List<Course>? Courses { get; set; }
    }
}
