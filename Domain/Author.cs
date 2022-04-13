using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MangaProject.BL.Domain
{
    public class Author
    {
        public Author()
        {
            
        }
        public Author(string name, DateTime birthday, int id=0)
        {
            Name = name;
            Birthday = birthday;
            Id = id;
        }
        
        public Author(string name, DateTime birthday, Gender gender, ICollection<MangaAuthor> mangas=null, int id=0)
        {
            Name = name;
            Birthday = birthday;
            Gender = gender;
            Mangas = mangas;
            Id = id;
        }
        
        [Required(ErrorMessage="name cannot be empty")]
        [StringLength(50, ErrorMessage="50 characters is the maximum allowed")]
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public Gender Gender { get; set; }
        public ICollection<MangaAuthor> Mangas{ get; set; }
        public int Id { get; set; }
    }
}