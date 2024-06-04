using System.ComponentModel.DataAnnotations;

namespace MovieAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Le titre est requis")]
        public string Title { get; set; }
        /*Microsoft.AspNetCore.Components.DataAnnotation.Validation*/
        [ValidateComplexType]
        public Person Realisator { get; set; }
        [Required]
        [Range(1977, 2050)]
        public int ReleaseYear { get; set; }

        public string Synopsis { get; set; }
        public Movie()
        {
            Realisator = new Person();
        }
    }

    public class Person
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
    }
}
