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

        public DbSet<Berita> Berita { get; set; }

        public DbSet<Acara> Acara { get; set; }

        public DbSet<Admin> Admins { get; set; }

        public DbSet<Library> Libraries { get; set; }

        public DbSet<AdmissionPackage> AdmissionPackages { get; set; }
        public DbSet<AdmissionItem> AdmissionItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique slug
            modelBuilder.Entity<Library>()
                .HasIndex(x => x.Slug)
                .IsUnique();

            // Item ikut kehapus saat delete package
            modelBuilder.Entity<AdmissionPackage>()
                .HasMany(p => p.AdmissionItems)
                .WithOne(i => i.AdmissionPackage)
                .HasForeignKey(i => i.AdmissionPackageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AdmissionItem>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);
        }
    }
}
