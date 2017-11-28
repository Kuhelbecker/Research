using CinemaMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CinemaMVC.Repositories
{
    public class MovieRepository : IRepository<Movie>, IDisposable
    {
        private readonly CinemaDbContext db = new CinemaDbContext();
        public Movie Item { get; set; }
        public IEnumerable<Movie> List => this.db.Movies.ToList();

        public void Save(Movie movie)
        {
            db.Movies.Add(movie);
            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}