using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MangaProject.BL.Domain
{
    [Owned]
    public class Protagonist : IValidatableObject
    {
        // As requested by professor De Keulenaar I've added a new class with a new relationship type (1-1) so that I don't have nothing but 1-many relations 
        [Required(ErrorMessage = "Protagonist Name cannot be empty")]
        public String Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }

        public Protagonist() {}

        public Protagonist(String name, int age, Gender gender)
        {
            Name = name;
            Age = age;
            Gender = gender;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (Age < 0)
            {
                string errorMessage = "Protagonist age cannot be negative";
                errors.Add(new ValidationResult(errorMessage, new string[] {"Age"}));
            }

            return errors;
        }
        
    }
}