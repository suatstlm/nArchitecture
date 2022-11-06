using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RentACar.Domain.Entities;

namespace RentACar.Persistance.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<RefreshToken> UserOperationClaims { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                base.OnConfiguring(
                    optionsBuilder.UseSqlServer(Configuration.GetConnectionString("ConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>(a =>
            {
                a.ToTable("Brands").HasKey(b => b.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasMany(p => p.Models);
            });

            modelBuilder.Entity<Model>(a =>
            {
                a.ToTable("Models").HasKey(m => m.Id);
                a.Property(m => m.Id).HasColumnName("Id");
                a.Property(m => m.BrandId).HasColumnName("BrandId");
                a.Property(m => m.Name).HasColumnName("Name");
                a.Property(m => m.DailyPrice).HasColumnName("DailyPrice");
                a.Property(m => m.ImageUrl).HasColumnName("ImageUrl");
                a.HasOne(m => m.Brand);
            });

            modelBuilder.Entity<User>(a =>
            {
                a.ToTable("Users").HasKey(u => u.Id);
                a.Property(u => u.Id).HasColumnName("Id");
                a.Property(u => u.FirstName).HasColumnName("FirstName");
                a.Property(u => u.LastName).HasColumnName("LastName");
                a.Property(u => u.Email).HasColumnName("Email");
                a.HasIndex(u => u.Email, "UK_Users_Email").IsUnique();
                a.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt");
                a.Property(u => u.PasswordHash).HasColumnName("PasswordHash");
                a.Property(u => u.Status).HasColumnName("Status").HasDefaultValue(true);
                a.Property(u => u.AuthenticatorType).HasColumnName("AuthenticatorType");

            });

            modelBuilder.Entity<OperationClaim>(a =>
            {
                a.ToTable("OperationClaims").HasKey(o => o.Id);
                a.Property(o => o.Id).HasColumnName("Id");
                a.Property(o => o.Name).HasColumnName("Name");
                a.HasIndex(o => o.Name, "UK_OperationClaims_Name").IsUnique();
            });

            modelBuilder.Entity<UserOperationClaim>(a =>
            {
                a.ToTable("UserOperationClaims").HasKey(u => u.Id);
                a.Property(u => u.Id).HasColumnName("Id");
                a.Property(u => u.UserId).HasColumnName("UserId");
                a.Property(u => u.OperationClaimId).HasColumnName("OperationClaimId");
                a.HasIndex(u => new { u.UserId, u.OperationClaimId }, "UK_UserOperationClaims_UserId_OperationClaimId").IsUnique();
                a.HasOne(u => u.User);
                a.HasOne(u => u.OperationClaim);
            });

            modelBuilder.Entity<RefreshToken>(a =>
            {
                a.ToTable("RefreshTokens").HasKey(r => r.Id);
                a.Property(r => r.Id).HasColumnName("Id");
                a.Property(r => r.UserId).HasColumnName("UserId");
                a.Property(r => r.Token).HasColumnName("Token");
                a.Property(r => r.Expires).HasColumnName("Expires");
                a.Property(r => r.Created).HasColumnName("Created");
                a.Property(r => r.CreatedByIp).HasColumnName("CreatedByIp");
                a.Property(r => r.Revoked).HasColumnName("Revoked");
                a.Property(r => r.RevokedByIp).HasColumnName("RevokedByIp");
                a.Property(r => r.ReplacedByToken).HasColumnName("ReplacedByToken");
                a.Property(r => r.ReasonRevoked).HasColumnName("ReasonRevoked");
                a.HasOne(r => r.User);
            });


            Brand[] brandEntitySeeds = { new(1, "BMW"), new(2, "Mercedes") };
            modelBuilder.Entity<Brand>().HasData(brandEntitySeeds);

            Model[] modelEntitySeeds = { new(1, 1, "Series 4", 1500, ""), new(2, 2, "A3", 1500, "") };
            modelBuilder.Entity<Model>().HasData(modelEntitySeeds);
        }
    }
}
