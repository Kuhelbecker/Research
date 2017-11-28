using CinemaMVC.Models;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace CinemaMVC.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<CinemaMVC.Models.CinemaDbContext>
    {
        private CinemaDbContext _context;
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CinemaDbContext context)
        {
            this._context = context;

            var movies = new List<Movie>
            {
                new Movie { Title = "Blade Runner"}
            };

            this.AddToContext(movies);

            context.SaveChanges();
        }

        private void AddToContext<TEntity>(IEnumerable<TEntity> entityList) where TEntity : class
        {
            this._context.Set<TEntity>().AddRange(entityList);
        }
    }
}