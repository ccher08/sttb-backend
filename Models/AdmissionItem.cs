using System.Text.Json.Serialization;

namespace SttbApi.Models
{
    public class AdmissionItem
    {
        public int Id { get; set; }

        public int AdmissionPackageId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        // navigation
        [JsonIgnore]
        public AdmissionPackage? AdmissionPackage { get; set; }
    }
}
