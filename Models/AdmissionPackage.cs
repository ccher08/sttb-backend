using System.Text.Json.Serialization;

namespace SttbApi.Models
{
    public class AdmissionPackage
    {
        public int Id { get; set; }

        public int ProgramStudiId { get; set; }
        public string Year { get; set; }

        // navigation
        [JsonIgnore]
        public ProgramStudi? ProgramStudi { get; set; }
        public ICollection<AdmissionItem> AdmissionItems { get; set; } = new List<AdmissionItem>();
    }
}
