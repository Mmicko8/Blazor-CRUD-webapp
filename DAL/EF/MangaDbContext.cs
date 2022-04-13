using System;
using System.Diagnostics;
using MangaProject.BL.Domain;
using Microsoft.EntityFrameworkCore;

namespace MangaProject.DAL.EF {
    public class MangaDbContext : DbContext {
        public MangaDbContext() {
            MangaInitializer.Initialize(this, dropCreateDatabase: false);
        }

        public MangaDbContext(DbContextOptions options) : base(options) {
            MangaInitializer.Initialize(this, dropCreateDatabase: false);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder
                    .UseSqlite("Data Source=../Manga_EF.db")
                    .LogTo(message => Debug.WriteLine(message))
                    ;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<MangaAuthor>().Property<int>("MangasFK");
            modelBuilder.Entity<MangaAuthor>().HasOne(ma => ma.Manga)
                .WithMany(m => m.Authors)
                .HasForeignKey("MangasFK")
                .IsRequired();

            modelBuilder.Entity<MangaAuthor>().Property<int>("AuthorsFK");
            modelBuilder.Entity<MangaAuthor>().HasOne(ma => ma.Author)
                .WithMany(a => a.Mangas)
                .HasForeignKey("AuthorsFK")
                .IsRequired();

            modelBuilder.Entity<Manga>().Property<int>("MagazinesFK");
            modelBuilder.Entity<Manga>().HasOne(m => m.Magazine)
                .WithMany(mag => mag.Mangas)
                .HasForeignKey("MagazinesFK")
                .IsRequired();

            modelBuilder.Entity<Manga>().Property<int>("AnimeFK");
            modelBuilder.Entity<Manga>().HasOne(m => m.Anime)
                .WithOne(a => a.Manga)
                .HasForeignKey<Anime>("AnimeFK")
                .IsRequired(false);
            
            modelBuilder.Entity<MangaAuthor>().HasKey("MangasFK", "AuthorsFK");
        }

        public DbSet<Manga> Mangas { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Magazine> Magazines { get; set; }
        public DbSet<MangaAuthor> MangaAuthors { get; set; }
        public DbSet<Anime> Animes { get; set; }
    }
}