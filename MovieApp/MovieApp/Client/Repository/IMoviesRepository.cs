using MovieApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieApp.Client.Repository
{
    public interface IMoviesRepository
    {
        Task<List<Movie>> GetMovies();
        Task<int> CreateMovie(Movie movie);
        Task DeleteMovie(int Id);
        Task<Movie> GetDetailsMovie(int id);
        Task UpdateMovie(Movie movie);
    }
}
