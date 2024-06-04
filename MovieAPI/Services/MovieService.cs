using MovieAPI.Models;
using System;

namespace MovieAPI.Services
{
    public class MovieService
    {
        public List<Movie> Liste { get; set; }
        public MovieService()
        {
            Liste = new List<Movie>();
            Liste.Add(new Movie
            {
                Id = 1,
                Title = "Star wars",
                Realisator = new Person
                {
                    Firstname = "George",
                    Lastname = "Lucas"
                },
                ReleaseYear = 1977,
                Synopsis = "Pan pan dans l'espace"

            });

            Liste.Add(new Movie
            {
                Id = 2,
                Title = "Pacific Rim",
                Realisator = new Person
                {
                    Firstname = "Guillermo",
                    Lastname = "Del Toro"
                },
                ReleaseYear = 2013,
                Synopsis = "Des gros robots et des monstres"

            });
        }

        public Movie GetById(int id)
        {
            return Liste.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Movie movie)
        {
            movie.Id = Liste.Max(x => x.Id) + 1;
            Liste.Add(movie);
        }
    }
}
