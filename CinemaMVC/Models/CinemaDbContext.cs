using System.Data.Entity;

namespace CinemaMVC.Models
{
    public class CinemaDbContext: DbContext
    {
        public DbSet<Movie> Movies { get; set; }
    }
}