using System.ComponentModel.DataAnnotations;

namespace MangaProject.BL.Domain {
    public class Anime {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public double? Rating { get; set; }
        public int Episodes { get; set; }
        public Manga Manga { get; set; }

        public Anime() { }

        public Anime(string title, int episodes, double? rating = null, int id = 0, Manga manga = null) {
            Title = title;
            Episodes = episodes;
            Rating = rating;
            Id = id;
            Manga = manga;
        }
    }
}