using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MangaProject.BL.Domain
{
    public class Manga : IValidatableObject
    {
        public Manga() {}

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
        public Magazine Magazine { get; set; }
        public ICollection<MangaAuthor> Authors { get; set; }
        public Anime Anime { get; set; }
        public int Id { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            ValidateDate(errors);
            ValidateVolumes(errors);
            ValidateAuthors(errors);
            ValidateProtagonist(errors);

            return errors;
        }

        private void ValidateDate(ICollection<ValidationResult> errors)
        {
            if (DateTime.Now < StartDate)
            {
                var errorMessage = "StartDate of manga must be in the past";
                errors.Add(new ValidationResult(errorMessage, new string[] {"StartDate"}));
            }
        }
        
        private void ValidateVolumes(ICollection<ValidationResult> errors)
        {
            if (Volumes < 0)
            {
                var errorMessage = "Volumes cannot be negative";
                errors.Add(new ValidationResult(errorMessage, new string[] {"Volumes"}));
            }
        }
        
        private void ValidateAuthors(ICollection<ValidationResult> errors)
        {
            if (Authors == null) return;
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

        private void ValidateProtagonist(List<ValidationResult> errors)
        {
            var protagonistErrors = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(Protagonist, new ValidationContext(Protagonist)
                , protagonistErrors, validateAllProperties: true);
            if (!valid)
            {
                errors.AddRange(protagonistErrors);
            }
        }
        
    }
}