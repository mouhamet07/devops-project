using gestionStock.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;
namespace gestionStock.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Enums
            modelBuilder.HasPostgresEnum<RoleType>();
            modelBuilder.HasPostgresEnum<EtatProduit>();
            // USER
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Id)
                    .HasColumnName("id");
                entity.Property(u => u.Email)
                    .HasColumnName("email")
                    .IsRequired();
                entity.Property(u => u.Password)
                    .HasColumnName("password")
                    .IsRequired();
                entity.Property(u => u.IsArchived)
                    .HasColumnName("is_archived");
                entity.Property(u => u.role)
                    .HasColumnName("role")
                    .HasColumnType("role_type")
                    .IsRequired();
            });
            // CATEGORIE
            modelBuilder.Entity<Categorie>(entity =>
            {
                entity.ToTable("categorie");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id)
                    .HasColumnName("id");
                entity.Property(c => c.Libelle)
                    .HasColumnName("libelle")
                    .IsRequired();
                entity.Property(c => c.IsArchived)
                    .HasColumnName("is_archived");
            });
            // PRODUIT
            modelBuilder.Entity<Produit>(entity =>
            {
                entity.ToTable("produit");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Id)
                    .HasColumnName("id");
                entity.Property(p => p.Libelle)
                    .HasColumnName("libelle");
                entity.Property(p => p.Quantite)
                    .HasColumnName("quantite");
                entity.Property(p => p.IsArchived)
                    .HasColumnName("is_archived");
                entity.Property(p => p.etat)
                    .HasColumnName("etat")
                    .HasColumnType("etat_produit");
                // relation avec categorie
                entity.HasOne(p => p.categorie)
                    .WithMany()
                    .HasForeignKey("categorie_id")
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString =
                "Host=ep-summer-haze-ank0wc57-pooler.c-6.us-east-1.aws.neon.tech;" +
                "Database=gestionStock;" +
                "Username=neondb_owner;" +
                "Password=npg_dbphFPiKaS46;" +
                "Ssl Mode=Require;" +
                "Trust Server Certificate=true";
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
            // mapping enums postgres
            dataSourceBuilder.MapEnum<RoleType>("role_type");
            dataSourceBuilder.MapEnum<EtatProduit>("etat_produit");
            var dataSource = dataSourceBuilder.Build();
            optionsBuilder.UseNpgsql(dataSource);
        }
    }
}