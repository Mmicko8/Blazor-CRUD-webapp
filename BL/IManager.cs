using System;
using System.Collections.Generic;
using MangaProject.BL.Domain;

namespace MangaProject.BL {
    public interface IManager {
        Manga GetManga(int id);
        Manga GetMangaWithAuthors(int id);
        IEnumerable<Manga> GetAllMangas();
        IEnumerable<Manga> GetAllMangasWithAuthors();
        IEnumerable<Manga> GetAllMangasWithMagazine();
        IEnumerable<Manga> GetAllMangasExceptOfAuthor(int authorId);
        IEnumerable<Manga> GetMangaByVolumesAndRating(int? volumes = null, double? rating = null);
        IEnumerable<Manga> GetMangasOfAuthor(int authorId);
        IEnumerable<Manga> GetMangasWithoutAnime();
        Manga AddManga(string title, DateTime startDate, int volumes, Protagonist protagonist, double? rating,
            ICollection<MangaAuthor> authors = null, Magazine magazine = null, Anime anime = null, int id = 0);
        void RemoveManga(int id);
        Protagonist GetProtagonistOfManga(int mangaid);
        
        Author GetAuthor(int id);
        IEnumerable<Author> GetAllAuthors();
        IEnumerable<Author> GetAllAuthorsWithBooks();
        IEnumerable<Author> GetAuthorsByGender(Gender gender);
        Author AddAuthor(string name, DateTime birthday, Gender gender, ICollection<MangaAuthor> mangas = null,
            int id = 0);
        void RemoveAuthor(int id);
        

        Magazine GetMagazine(int id);
        IEnumerable<Magazine> GetAllMagazines();
        Magazine AddMagazine(string name, Schedule schedule, ICollection<Manga> mangas = null, int id = 0);
        Magazine ChangeMagazine(int id, string newName, Schedule newSchedule);
        void RemoveMagazine(int magazineId);
        
        
        MangaAuthor AddMangaAuthor(int mangaId, int authorId, ContributionType contributionType);
        void RemoveMangaAuthor(int mangaId, int authorId);
        ContributionType GetContributionType(int mangaId, int authorId);
        
        
        Anime GetAnime(int id);
        IEnumerable<Anime> GetAllAnime();
        Anime AddAnime(string title, int episodes, double? rating, int id = 0);
        void AddAnimeToManga(int mangaId, int animeId);
        Anime ChangeAnime(int id, string title = null, int? episodes = null, double? rating = null, int? mangaId = null);
    }
}