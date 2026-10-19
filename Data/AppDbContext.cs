using Microsoft.EntityFrameworkCore;
using NablusWS.Entities;

namespace NablusWS.Data;

public class AppDbContext : DbContext
{
    public DbSet<Municipality> Municipalities => Set<Municipality>();
    public DbSet<WasteCollectionSite> WasteCollectionSites => Set<WasteCollectionSite>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Language> Languages => Set<Language>();
    public DbSet<SiteTranslation> SiteTranslations => Set<SiteTranslation>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(
            @"Server=.\SQLEXPRESS;Database=NablusWasteDb;Trusted_Connection=True;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Municipality>(e =>
        {
            e.Property(m => m.Name).IsRequired().HasMaxLength(150);
            e.Property(m => m.Phone).HasMaxLength(20);
            e.Property(m => m.Address).HasMaxLength(250);
        });

        modelBuilder.Entity<WasteCollectionSite>(e =>
        {
            e.Property(s => s.Code).IsRequired().HasMaxLength(20);
            e.HasIndex(s => s.Code).IsUnique();
            e.Property(s => s.Address).HasMaxLength(250);

            e.HasOne(s => s.Municipality)
             .WithMany(m => m.Sites)
             .HasForeignKey(s => s.MunicipalityId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Employee>(e =>
        {
            e.Property(x => x.FullName).IsRequired().HasMaxLength(150);
            e.Property(x => x.Phone).HasMaxLength(20);

            e.HasOne(x => x.Site)
             .WithOne(s => s.Employee)
             .HasForeignKey<Employee>(x => x.SiteId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasIndex(x => x.SiteId).IsUnique();   // one employee per site
        });

        modelBuilder.Entity<Language>(e =>
        {
            e.Property(l => l.Code).IsRequired().HasMaxLength(10);
            e.HasIndex(l => l.Code).IsUnique();
            e.Property(l => l.Name).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<SiteTranslation>(e =>
        {
            e.HasKey(t => new { t.SiteId, t.LanguageId });   // composite PK
            e.Property(t => t.Name).IsRequired().HasMaxLength(200);

            e.HasOne(t => t.Site)
             .WithMany(s => s.Translations)
             .HasForeignKey(t => t.SiteId)
             .OnDelete(DeleteBehavior.Cascade);              

            e.HasOne(t => t.Language)
             .WithMany(l => l.SiteTranslations)
             .HasForeignKey(t => t.LanguageId)
             .OnDelete(DeleteBehavior.Restrict);             
        });

        // SEED DATA 
        modelBuilder.Entity<Municipality>().HasData(
            new Municipality { Id = 1, Name = "Nablus Municipality", Phone = "09-2384000", Address = "Nablus, Palestine" });

        modelBuilder.Entity<Language>().HasData(
            new Language { Id = 1, Code = "ar", Name = "العربية" },
            new Language { Id = 2, Code = "en", Name = "English" });

        modelBuilder.Entity<WasteCollectionSite>().HasData(
            new WasteCollectionSite { Id = 1, Code = "NBL-RAF", Address = "Rafidia, Nablus",    MunicipalityId = 1 },
            new WasteCollectionSite { Id = 2, Code = "NBL-MAK", Address = "Al-Makhfiya, Nablus", MunicipalityId = 1 },
            new WasteCollectionSite { Id = 3, Code = "NBL-OLD", Address = "Old City, Nablus",   MunicipalityId = 1 });

        modelBuilder.Entity<Employee>().HasData(
            new Employee { Id = 1, FullName = "Ahmad Khalil", Phone = "0599111111", SiteId = 1 },
            new Employee { Id = 2, FullName = "Omar Nasser",  Phone = "0599222222", SiteId = 2 },
            new Employee { Id = 3, FullName = "Lina Saleh",   Phone = "0599333333", SiteId = 3 });

        modelBuilder.Entity<SiteTranslation>().HasData(
            new SiteTranslation { SiteId = 1, LanguageId = 1, Name = "موقع رفيديا لجمع النفايات" },
            new SiteTranslation { SiteId = 1, LanguageId = 2, Name = "Rafidia Waste Collection Site" },
            new SiteTranslation { SiteId = 2, LanguageId = 1, Name = "موقع المخفية لجمع النفايات" },
            new SiteTranslation { SiteId = 2, LanguageId = 2, Name = "Al-Makhfiya Waste Collection Site" },
            new SiteTranslation { SiteId = 3, LanguageId = 1, Name = "موقع البلدة القديمة لجمع النفايات" },
            new SiteTranslation { SiteId = 3, LanguageId = 2, Name = "Old City Waste Collection Site" });
    }
}