using Humanizer.Localisation;
using Microsoft.EntityFrameworkCore;
using MovieApp.Server.Data;
using MovieApp.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Server.Services
{
    public interface IMoviesRepository
    {

    }

    public interface IMoviesDbService
    {
        Task<List<Movie>> GetMovies();
        Task AddMovie(Movie movie);
        Task<Movie> GetMovie(int movieId);
        Task DeleteMovie(int id);
        Task UpdateMovie(Movie movie);
    }

    public class MoviesDbService : IMoviesDbService
    {
        private ApplicationDbContext _context;

        public MoviesDbService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddMovie(Movie movie)
        {
            _context.Add(movie);
            await _context.SaveChangesAsync();
        }

        public Task<Movie> GetMovie(int movieId)
        {
           return _context.Movies.FirstOrDefaultAsync(x => x.Id == movieId);
        }

        public Task<List<Movie>> GetMovies()
        {
            return _context.Movies.OrderBy(m => m.Title).ToListAsync();
        }

        public async Task DeleteMovie(int movieId)
        {
            var movie = _context.Movies.Find(movieId);
            _context.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMovie(Movie movie)
        {
            _context.Attach(movie).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
