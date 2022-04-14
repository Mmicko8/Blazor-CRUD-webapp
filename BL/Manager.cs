using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MangaProject.BL.Domain;
using MangaProject.DAL;

namespace MangaProject.BL
{
    public class Manager : IManager
    {
        private readonly IRepository _repo;

        public Manager(IRepository repository)
        {
            _repo = repository;
        }

        public Manga GetManga(int id)
        {
            return _repo.ReadManga(id);
        }

        public Manga GetMangaWithAuthors(int id)
        {
            return _repo.ReadMangaWithAuthors(id);
        }

        public IEnumerable<Manga> GetAllMangas()
        {
            return _repo.ReadAllMangas();
        }

        public IEnumerable<Manga> GetAllMangasWithAuthors()
        {
            return _repo.ReadAllMangasWithAuthors();
        }

        public IEnumerable<Manga> GetAllMangasWithMagazine()
        {
            return _repo.ReadAllMangasWithMagazine();
        }

        public IEnumerable<Manga> GetAllMangasExceptOfAuthor(int authorId)
        {
            return _repo.ReadAllMangasExceptOfAuthor(authorId);
        }

        public IEnumerable<Manga> GetMangaByVolumesAndRating(int? volumes = null, double? rating = null)
        {
            return _repo.ReadMangasByVolumesAndRating(volumes, rating);
        }

        public IEnumerable<Manga> GetMangasOfAuthor(int authorId)
        {
            return _repo.ReadMangasOfAuthor(authorId);
        }

        public IEnumerable<Manga> GetMangasWithoutAnime()
        {
            return _repo.ReadMangasWithoutAnime();
        }

        public Manga AddManga(string title, DateTime startDate, int volumes, Protagonist protagonist,
            double? rating = null, ICollection<MangaAuthor> authors = null,
            Magazine magazine = null, Anime anime = null, int id = 0)
        {
            if (magazine == null)
                magazine = new Magazine($"Unknown magazine of {title}", Schedule.Other);
            Manga manga = new Manga(title, startDate, volumes, protagonist, rating, authors, magazine, anime, id);
            ValidateManga(manga);
            _repo.CreateManga(manga);
            return manga;
        }

        public void RemoveManga(int id)
        {
            _repo.DeleteManga(id);
        }

        public Author GetAuthor(int id)
        {
            return _repo.ReadAuthor(id);
        }

        public IEnumerable<Author> GetAllAuthors()
        {
            return _repo.ReadAllAuthors();
        }

        public IEnumerable<Author> GetAllAuthorsWithBooks()
        {
            return _repo.ReadAllAuthorsWithManga();
        }

        public IEnumerable<Author> GetAuthorsByGender(Gender gender)
        {
            return _repo.ReadAuthorsByGender(gender);
        }

        public Author AddAuthor(string name, DateTime birthday, Gender gender, ICollection<MangaAuthor> mangas = null,
            int id = 0)
        {
            Author author = new Author(name, birthday, gender, mangas, id);
            ValidateAuthor(author);
            _repo.CreateAuthor(author);
            return author;
        }

        public void RemoveAuthor(int id)
        {
            _repo.DeleteAuthor(id);
        }

        public Magazine GetMagazine(int id)
        {
            return _repo.ReadMagazine(id);
        }

        public IEnumerable<Magazine> GetAllMagazines()
        {
            return _repo.ReadAllMagazines();
        }

        public Magazine AddMagazine(string name, Schedule schedule, ICollection<Manga> mangas = null, int id = 0)
        {
            Magazine magazine = new Magazine(name, schedule, mangas, id);
            ValidateMagazine(magazine);
            _repo.CreateMagazine(magazine);
            return magazine;
        }

        public Magazine ChangeMagazine(int id, string newName, Schedule newSchedule)
        {
            Magazine magazine = GetMagazine(id);
            magazine.Name = newName;
            magazine.Schedule = newSchedule;
            ValidateMagazine(magazine);
            _repo.UpdateMagazine(magazine);
            return magazine;
        }

        public void RemoveMagazine(int magazineId)
        {
            _repo.DeleteMagazine(magazineId);
        }

        public MangaAuthor AddMangaAuthor(int mangaId, int authorId, ContributionType contributionType)
        {
            foreach (var mangaOfAuthor in _repo.ReadMangasOfAuthor(authorId))
            { // check if relation already exists
                if (mangaOfAuthor.Id == mangaId)
                {
                    return null;
                }
            }
            MangaAuthor mangaAuthor = new MangaAuthor(contributionType, GetManga(mangaId), GetAuthor(authorId));
            _repo.CreateMangaAuthor(mangaAuthor);
            return mangaAuthor;
        }

        public void RemoveMangaAuthor(int mangaId, int authorId)
        {
            _repo.DeleteMangaAuthor(mangaId, authorId);
        }

        public ContributionType GetContributionType(int mangaId, int authorId)
        {
            return _repo.ReadContributionOfMangaAuthor(mangaId, authorId);
        }

        public Protagonist GetProtagonistOfManga(int mangaid)
        {
            return _repo.ReadProtagonistOfManga(mangaid);
        }

        public Anime GetAnime(int id) {
            return _repo.ReadAnime(id);
        }

        public IEnumerable<Anime> GetAllAnime() {
            return _repo.ReadAllAnime();
        }

        public Anime AddAnime(string title, int episodes, double? rating, int id = 0) {
            Anime anime = new Anime(title, episodes, rating, id);
            _repo.CreateAnime(anime);
            return anime;
        }

        public void AddAnimeToManga(int mangaId, int animeId)
        {
            _repo.AddAnimeToManga(mangaId, animeId);
        }

        public Anime ChangeAnime(int id, string title = null, int? episodes = null, double? rating = null, int? mangaId = null)
        {
            var anime = GetAnime(id);
            if (title != null)
                anime.Title = title;
            if (episodes != null)
                anime.Episodes = (int) episodes;
            if (rating != null)
                anime.Rating = rating;
            anime.Manga = mangaId == null ? null : GetManga((int) mangaId);

            _repo.UpdateAnime(anime);

            return anime;
        }

        private void ValidateManga(Manga manga)
        {
            Validate(manga);
            Validate(manga.Protagonist);
        }

        private void ValidateAuthor(Author author)
        {
            Validate(author);
        }

        private void ValidateMagazine(Magazine magazine)
        {
            Validate(magazine);
        }
        
        private void Validate<T>(T instance)
        {
            List<ValidationResult> errors = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(instance, new ValidationContext(instance)
                , errors, validateAllProperties: true);
            if (!valid)
            {
                string errorsString = "";
                foreach (ValidationResult validationResult in errors)
                {
                    errorsString += validationResult.ErrorMessage + "\t";
                }

                throw new ValidationException(errorsString);
            }
        }
    }
}