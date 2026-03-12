namespace SttbApi.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ProgramStudi
    {
        public int Id { get; set; }

        public string Slug { get; set; }

        public string Level { get; set; }

        public string Name { get; set; }

        public string? Duration { get; set; }

        public int? TotalCredits { get; set; }

        public string? Description { get; set; }

        public string Image { get; set; }

        public List<string> Highlights { get; set; } = new();

        public List<MataKuliah>? MataKuliah { get; set; }

        public List<CurriculumGroup>? Curriculum { get; set; }

        public List<OverviewAbout>? Abouts { get; set; }

        public List<OverviewRequirement>? Requirements { get; set; }

        public List<CompetencyGroup>? Competencies { get; set; }

        public string? HeroTitle { get; set; }
        public string? HeroSubtitle { get; set; }
        public string? Degree { get; set; }


    }
}