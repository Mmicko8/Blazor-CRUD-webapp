using System;
using System.Collections.Generic;
using System.Linq;
using MangaProject.BL.Domain;
using Microsoft.EntityFrameworkCore;

namespace MangaProject.DAL.EF
{
    public class Repository : IRepository
    {
        private readonly MangaDbContext _context;

        public Repository()
        {
            _context = new MangaDbContext();
        }
        public Manga ReadManga(int id)
        {
            return _context.Mangas.Find(id);
        }

        public Manga ReadMangaWithAuthors(int id)
        {
            return _context.Mangas.Include(m => m.Authors)
                .ThenInclude(ma => ma.Author)
                .Single(m => m.Id == id);
        }
        
        public IEnumerable<Manga> ReadAllMangas()
        {
            return _context.Mangas.AsEnumerable();
        }

        public IEnumerable<Manga> ReadAllMangasWithAuthors()
        {
            return _context.Mangas.Include(m => m.Authors)
                .ThenInclude(ma => ma.Author).AsEnumerable();
        }

        public IEnumerable<Manga> ReadAllMangasWithMagazine()
        {
            return _context.Mangas.Include(m => m.Magazine).AsEnumerable();
        }

        public IEnumerable<Manga> ReadAllMangasExceptOfAuthor(int authorId)
        {
            ICollection<int> mangasOfAuthor = _context.MangaAuthors.Where(ma => ma.Author.Id == authorId)
                .Select(ma => ma.Manga.Id).ToList();

            return _context.Mangas.Where(m => !mangasOfAuthor.Contains(m.Id)).AsEnumerable();
        }

        public IEnumerable<Manga> ReadMangasByVolumesAndRating(int? volumes = null, double? rating = null)
        {
            IQueryable<Manga> result = _context.Mangas;
            if (volumes != null) 
                result = result.Where(manga => manga.Volumes >= volumes);
            
            if (rating != null)
                result = result.Where(manga => manga.Rating != null && manga.Rating >= rating); 

            return result.AsEnumerable();
        }

        public IEnumerable<Manga> ReadMangasOfAuthor(int authorId)
        {
            return _context.MangaAuthors.Where(ma => ma.Author.Id == authorId).Select(ma => ma.Manga);
            // return _context.Mangas.Where(m => m.Authors.Any(ma => ma.Author.Id ==authorId)); // alternative method
        }

        public IEnumerable<Manga> ReadMangasWithoutAnime()
        {
            return _context.Mangas.Where(m => m.Anime == null);
        }

        public void CreateManga(Manga manga)
        {
            _context.Mangas.Add(manga);
            _context.SaveChanges();
        }

        public void DeleteManga(int id)
        {
            _context.Mangas.Remove(_context.Mangas.Find(id));
            _context.SaveChanges();
        }

        public Author ReadAuthor(int id)
        {
            return _context.Authors.Find(id);
        }

        public IEnumerable<Author> ReadAllAuthors()
        {
            return _context.Authors.AsEnumerable();
        }

        public IEnumerable<Author> ReadAllAuthorsWithManga()
        {
            return _context.Authors.Include(a => a.Mangas)
                .ThenInclude(ma => ma.Manga).AsEnumerable();
        }

        public IEnumerable<Author> ReadAuthorsByGender(Gender gender)
        {
            return _context.Authors.Where(author => author.Gender == gender).AsEnumerable();
        }

        public void CreateAuthor(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public void DeleteAuthor(int id)
        {
            _context.Authors.Remove(_context.Authors.Find(id));
            _context.SaveChanges();
        }

        public Magazine ReadMagazine(int id)
        {
            return _context.Magazines.Find(id);
        }

        public IEnumerable<Magazine> ReadAllMagazines()
        {
            return _context.Magazines.AsEnumerable();
        }

        public void CreateMagazine(Magazine magazine)
        {
            _context.Magazines.Add(magazine);
            _context.SaveChanges();
        }

        public Magazine UpdateMagazine(Magazine magazine)
        {
            _context.Magazines.Update(magazine);
            _context.SaveChanges();
            return magazine;
        }

        public void DeleteMagazine(int magazineId)
        {
            _context.Magazines.Remove(_context.Magazines.Find(magazineId));
            _context.SaveChanges();
        }

        public void CreateMangaAuthor(MangaAuthor mangaAuthor)
        {
            try
            {
                _context.MangaAuthors.Add(mangaAuthor);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _context.ChangeTracker.Clear();
                throw new ArgumentException($"MangaAuthor is not valid: {e.Message}");
            }
        }

        public void DeleteMangaAuthor(int mangaId, int authorId)
        {
            var temp = _context.MangaAuthors.Single(ma => ma.Manga.Id == mangaId && ma.Author.Id == authorId);
            _context.MangaAuthors.Remove(temp);
            _context.SaveChanges();
        }

        public ContributionType ReadContributionOfMangaAuthor(int mangaId, int authorId)
        {
            return _context.MangaAuthors
                .Single(ma => ma.Manga.Id == mangaId && ma.Author.Id == authorId)
                .ContributionType;
        }

        public Protagonist ReadProtagonistOfManga(int mangaId)
        {
            return _context.Mangas.Find(mangaId).Protagonist;
        }

        public Anime ReadAnime(int id)
        {
            return _context.Animes.Include(a => a.Manga).Single(a => a.Id == id);
        }

        public IEnumerable<Anime> ReadAllAnime() {
            return _context.Animes.Include(a => a.Manga).AsEnumerable();
        }

        public void CreateAnime(Anime anime) {
            _context.Animes.Add(anime);
            _context.SaveChanges();
        }

        // overwrites existing relation
        public void AddAnimeToManga(int mangaId, int animeId)
        {
            var anime = _context.Animes.Find(animeId);
            var manga = _context.Mangas.Find(mangaId);
            manga.Anime = anime;
            anime.Manga = manga;
            _context.SaveChanges();
        }

        public Anime UpdateAnime(Anime anime)
        {
            _context.Animes.Update(anime);
            _context.SaveChanges();
            return anime;
        }
    }
}