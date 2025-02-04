using MovieCRUD.DataAccess.Entity;

namespace MovieCRUD.Repository.Services;

public interface IMovieRepository
{
    Task<Guid> AddMovieAsync(Movie movie);
    Task DeleteMovieAsync(Guid id);
    Task UpdateMovieAsync(Movie movie);
    Task<Movie> GetMovieByIdAsync(Guid id);
    Task<List<Movie>> GetAllMoviesAsync();   
}