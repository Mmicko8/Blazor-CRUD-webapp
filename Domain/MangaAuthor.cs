using System.ComponentModel.DataAnnotations;

namespace MangaProject.BL.Domain
{
    public class MangaAuthor
    {
        [Required]
        public ContributionType ContributionType{ get; set; }
        [Required]
        public Manga Manga { get; set; }
        [Required]
        public Author Author { get; set; }

        public MangaAuthor() {}

        public MangaAuthor(ContributionType contributionType, Manga manga, Author author)
        {
            ContributionType = contributionType;
            Manga = manga;
            Author = author;
        }
    }
}