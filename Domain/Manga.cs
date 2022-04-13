using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MangaProject.BL.Domain
{
    public class Manga : IValidatableObject
    {
        public Manga()
        {
        }

        public Manga(string title, DateTime startDate, int volumes, double? rating = null, int id = 0)
        {
            Title = title;
            StartDate = startDate;
            Volumes = volumes;
            Rating = rating;
            Id = id;
        }

        public Manga(string title, DateTime startDate, int volumes, Protagonist protagonist, double? rating = null,
            ICollection<MangaAuthor> authors = null, Magazine magazine = null, Anime anime = null, int id = 0)
        {
            Title = title;
            StartDate = startDate;
            Volumes = volumes;
            Protagonist = protagonist;
            Rating = rating;
            Authors = authors;
            Magazine = magazine;
            Id = id;
            Anime = anime;
        }

        [Required(ErrorMessage = "title cannot be empty")]
        [StringLength(100, ErrorMessage = "100 characters is the maximum allowed for a manga title")]
        public string Title { get; set; }

        public DateTime StartDate { get; set; }
        public int Volumes { get; set; }

        [Required(ErrorMessage = "Manga needs a protagonist")]
        public Protagonist Protagonist { get; set; }

        [Range(0, 10)] public double? Rating { get; set; }

        // [Required(ErrorMessage = "Manga must have a magazine")]
        public Magazine Magazine { get; set; }
        public ICollection<MangaAuthor> Authors { get; set; }
        public Anime Anime { get; set; }
        public int Id { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (DateTime.Now < StartDate)
            {
                string errorMessage = "StartDate of manga must be in the past";
                errors.Add(new ValidationResult(errorMessage, new string[] {"StartDate"}));
            }

            if (Volumes < 0)
            {
                string errorMessage = "Volumes cannot be negative";
                errors.Add(new ValidationResult(errorMessage, new string[] {"Volumes"}));
            }

            if (Authors != null)
            {
                foreach (var author in Authors)
                {
                    if (StartDate < author.Author.Birthday)
                    {
                        string errorMessage = "StartDate of manga can't be earlier than any of its authors birthdays";
                        errors.Add(
                            new ValidationResult(errorMessage, new string[] {nameof(StartDate), nameof(Authors)}));
                    }
                }
            }

            List<ValidationResult> protagonistErrors = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(Protagonist, new ValidationContext(Protagonist)
                , protagonistErrors, validateAllProperties: true);
            if (!valid)
            {
                errors.AddRange(protagonistErrors);
            }

            return errors;
        }
    }
}