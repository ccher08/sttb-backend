using System.Text.Json.Serialization;

namespace SttbApi.Models
{
    public class Course
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Credits { get; set; }

        public int CurriculumGroupId { get; set; }

        [JsonIgnore]
        public CurriculumGroup? CurriculumGroup { get; set; }
    }
}
