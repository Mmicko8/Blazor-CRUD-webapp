using System.Collections.Generic;
using MangaProject.BL.Domain;

namespace MangaProject.DAL
{
    public interface IRepository
    {
        Manga ReadManga(int id);
        Manga ReadMangaWithAuthors(int id);
        IEnumerable<Manga> ReadAllMangas();
        IEnumerable<Manga> ReadAllMangasWithAuthors();
        IEnumerable<Manga> ReadAllMangasWithMagazine();
        IEnumerable<Manga> ReadAllMangasExceptOfAuthor(int authorId);
        IEnumerable<Manga> ReadMangasByVolumesAndRating(int? volumes = null, double? rating = null);
        IEnumerable<Manga> ReadMangasOfAuthor(int authorId);
        IEnumerable<Manga> ReadMangasWithoutAnime();
        void CreateManga(Manga manga);
        void DeleteManga(int id);
        Author ReadAuthor(int id);
        IEnumerable<Author> ReadAllAuthors();
        IEnumerable<Author> ReadAllAuthorsWithManga();
        IEnumerable<Author> ReadAuthorsByGender(Gender gender);
        void CreateAuthor(Author author);
        Magazine ReadMagazine(int id);
        IEnumerable<Magazine> ReadAllMagazines();
        void CreateMagazine(Magazine magazine);
        Magazine UpdateMagazine(Magazine magazine);
        void DeleteMagazine(int magazineId);
        void CreateMangaAuthor(MangaAuthor mangaAuthor);
        void DeleteMangaAuthor(int mangaId, int authorId);
        ContributionType ReadContributionOfMangaAuthor(int mangaId, int authorId);
        Protagonist ReadProtagonistOfManga(int mangaId);
        Anime ReadAnime(int id);
        IEnumerable<Anime> ReadAllAnime();
        void CreateAnime(Anime anime);
        void AddAnimeToManga(int mangaId, int animeId);
        Anime UpdateAnime(Anime anime);
    }
}