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

        // 🔥 MEDIA CMS
        public DbSet<Media> Media { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<Video> Video { get; set; }
        public DbSet<Journal> Journal { get; set; }
        public DbSet<Bulletin> Bulletin { get; set; }
        public DbSet<Monograph> Monograph { get; set; }
        public DbSet<Category> Category { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================
            // 🔹 EXISTING CONFIG
            // =========================

            modelBuilder.Entity<Library>()
                .HasIndex(x => x.Slug)
                .IsUnique();

            modelBuilder.Entity<AdmissionPackage>()
                .HasMany(p => p.AdmissionItems)
                .WithOne(i => i.AdmissionPackage)
                .HasForeignKey(i => i.AdmissionPackageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AdmissionItem>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            // =========================
            // 🔥 MEDIA RELATIONS
            // =========================

            // Media - Category (Many to One)
            modelBuilder.Entity<Media>()
                .HasOne(m => m.Category)
                .WithMany(c => c.Medias)
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // Slug unique
            modelBuilder.Entity<Media>()
                .HasIndex(m => m.Slug)
                .IsUnique();

            // =========================
            // 🔗 ONE-TO-ONE RELATIONS
            // =========================

            modelBuilder.Entity<Media>()
                .HasOne(m => m.Article)
                .WithOne(a => a.Media)
                .HasForeignKey<Article>(a => a.MediaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Media>()
                .HasOne(m => m.Video)
                .WithOne(v => v.Media)
                .HasForeignKey<Video>(v => v.MediaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Media>()
                .HasOne(m => m.Journal)
                .WithOne(j => j.Media)
                .HasForeignKey<Journal>(j => j.MediaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Media>()
                .HasOne(m => m.Bulletin)
                .WithOne(b => b.Media)
                .HasForeignKey<Bulletin>(b => b.MediaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Media>()
                .HasOne(m => m.Monograph)
                .WithOne(mo => mo.Media)
                .HasForeignKey<Monograph>(mo => mo.MediaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}