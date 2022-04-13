using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MangaProject.BL.Domain
{
    public class Magazine
    {
        public Magazine() {}
        public Magazine(string name, Schedule schedule, ICollection<Manga> mangas=null, int id=0)
        {
            Name = name;
            Schedule = schedule;
            Mangas = mangas;
            Id = id;
        }
        [Required(ErrorMessage = "magazine name cannot be empty")]
        [StringLength(50, ErrorMessage = "50 characters is the maximum allowed for a magazine name")]
        public string Name { get; set; }
        public Schedule Schedule { get; set; }
        public ICollection<Manga> Mangas { get; set; }
        public int Id { get; set; }
    }
}