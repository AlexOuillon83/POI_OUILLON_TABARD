using Isen.DotNet.Library.Models.Implementation;
using Microsoft.EntityFrameworkCore;

namespace Isen.DotNet.Library.Data
{
    public class ApplicationDbContext : DbContext
    {
        // 1 - Préciser les DbSet
        public DbSet<City> CityCollection { get;set; }
        public DbSet<Person> PersonCollection { get;set; }
        public DbSet<Commune> CommuneCollection { get; set; }
        public DbSet<Categorie> CategorieCollection { get; set; }
        public DbSet<Adresse> AdresseCollection { get; set; }
        public DbSet<PointInteret> PointInteretCollection { get; set; }

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(
            ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // 2 - Configurer les mappings tables / classes
            builder.Entity<City>()
                .ToTable("City")
                .HasMany(c => c.PersonCollection)
                .WithOne(p => p.City)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Person>()
                .ToTable("Person")
                .HasOne(p => p.City)
                .WithMany(c => c.PersonCollection)
                .HasForeignKey(p => p.CityId);

            builder.Entity<Commune>()
                .ToTable("Commune");

            builder.Entity<Categorie>()
                .ToTable("Categorie");

            builder.Entity<PointInteret>()
<<<<<<< HEAD
                .ToTable("PointInteret")
                .HasOne(a => a.Adresse); 
                
=======
                .ToTable("PointInteret");

>>>>>>> ece2764a4c1c2c4ea454622df992553bc8347c1a
            builder.Entity<Adresse>()
                .ToTable("Adresse")
                .HasOne(p => p.Commune);
        }
    }
}