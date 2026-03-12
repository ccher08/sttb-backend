using Microsoft.EntityFrameworkCore;
using SttbApi.Models;

namespace SttbApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProgramStudi> ProgramStudi { get; set; }

        public DbSet<MataKuliah> MataKuliah { get; set; }

        public DbSet<CurriculumGroup> CurriculumGroups { get; set; }

        public DbSet<Course> Courses { get; set; }

        public DbSet<OverviewAbout> OverviewAbouts { get; set; }

        public DbSet<OverviewRequirement> OverviewRequirements { get; set; }

        public DbSet<CompetencyGroup> CompetencyGroups { get; set; }

        public DbSet<CompetencyItem> CompetencyItems { get; set; }

        public DbSet<Berita> Beritas { get; set; }

        public DbSet<Admin> Admins { get; set; }
    }
}
