using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
   public class MovieShopDbContext: DbContext
    {
        // DbSets as properties
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options): base(options)
        {
                
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<Crew> Crews { get; set; }
        public DbSet<MovieCrew> MovieCrews { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        // to use fluent API we need to override a nmethod OnModelCreating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<MovieCrew>(ConfigureMovieCrew);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<Favorite>(ConfigureFavorite);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<UserRole>(ConfigureUserRole);
            modelBuilder.Entity<Cast>(ConfigureCast);
        }
        private void ConfigureCast(EntityTypeBuilder<Cast> builder) {
            builder.Property(t => t.Name).HasMaxLength(128);
            builder.Property(t => t.Gender).HasColumnType("nvarchar(MAX)");
            builder.Property(t => t.TmdbUrl).HasColumnType("nvarchar(MAX)");
            builder.Property(t => t.ProfilePath).HasMaxLength(2084);
        }
        private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder) {
            builder.HasKey(t => new { t.RoleId, t.UserId});
            builder.HasOne(t => t.User).WithMany(p => p.UserRoles).HasForeignKey(t => t.UserId);
            builder.HasOne(t => t.Role).WithMany(p => p.UserRoles).HasForeignKey(t => t.RoleId);
        }
        private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder) {
            builder.HasKey(t => t.Id);
            builder.HasOne(t => t.User).WithMany(p => p.Purchases).HasForeignKey(t => t.UserId);
            builder.HasOne(t => t.Movie).WithMany(p => p.Purchases).HasForeignKey(t => t.MovieId);
            builder.Property(t => t.TotalPrice).HasColumnType("decimal(18,2)");
            builder.Property(t => t.PurchaseNumber).HasColumnType("uniqueidentifier");
            builder.Property(t => t.PurchaseDateTime).HasColumnType("datetime2(7)");
        }
        private void ConfigureFavorite(EntityTypeBuilder<Favorite> builder) {
            builder.HasKey(t => t.Id);
            builder.HasOne(t => t.Movie).WithMany(p => p.Favorites).HasForeignKey(t => t.MovieId);
            builder.HasOne(t => t.User).WithMany(p => p.Favorites).HasForeignKey(t => t.UserId);
        }
        private void ConfigureReview(EntityTypeBuilder<Review> builder) {
            builder.HasKey(t => new { t.MovieId, t.UserId });
            builder.HasOne(t => t.Movie).WithMany(p => p.Reviews).HasForeignKey(t => t.MovieId);
            builder.HasOne(t => t.User).WithMany(p => p.Reviews).HasForeignKey(t => t.UserId);
        }
        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.HasKey(t => new { t.MovieId, t.GenreId });
            builder.HasOne(t => t.Movie).WithMany(p => p.MovieGenres).HasForeignKey(t=>t.MovieId);
            builder.HasOne(t => t.Genre).WithMany(p => p.MovieGenres).HasForeignKey(t => t.GenreId);
        }
        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder){
            builder.HasKey(t => new { t.CastId, t.MovieId, t.Character });
            builder.Property(t => t.Character).HasMaxLength(450);
        }

        private void ConfigureMovieCrew(EntityTypeBuilder<MovieCrew> builder) 
        {
            builder.ToTable("MovieCrew");
            builder.HasKey(t =>new { t.CrewId,t.MovieId,t.Department,t.Job});
            builder.Property(t => t.Department).HasMaxLength(128).IsRequired();
            builder.Property(t => t.Job).HasMaxLength(128).IsRequired();
            builder.HasOne(t => t.Crew).WithMany(p => p.MovieCrews).HasForeignKey(t => t.CrewId);
            builder.HasOne(t => t.Movie).WithMany(p => p.MovieCrews).HasForeignKey(t => t.MovieId);
        }
        private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
        {
            builder.ToTable("Trailer");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
            builder.Property(t => t.Name).HasMaxLength(2084);
        }
        private void ConfigureMovie (EntityTypeBuilder<Movie> builder)
        {
            // specify all the Fluent API rules for this model
            builder.ToTable("Movie");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).HasMaxLength(256).IsRequired();
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);
            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.Budget).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.Revenue).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");
            builder.Ignore(m => m.Rating);
        }

    }
}
