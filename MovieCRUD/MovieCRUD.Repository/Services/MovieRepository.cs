using MovieCRUD.DataAccess.Entity;
using System.Text.Json;

namespace MovieCRUD.Repository.Services;

public class MovieRepository : IMovieRepository
{
    private readonly string  _path;
    private List<Movie> _movies;
    public MovieRepository()
    {
        _path = Path.Combine(Directory.GetCurrentDirectory(), "Movies.json");
        if (!File.Exists(_path))
        {
            File.WriteAllText(_path, "[]");
        }
        _movies = GetAllMoviesAsync().Result;
    }
    public async Task<Guid> AddMovieAsync(Movie movie)
    {
        _movies.Add(movie);
        await SaveDataAsync();
        return movie.Id;
    }

    public async Task DeleteMovieAsync(Guid id)
    {
        var deletingMovie = await GetMovieByIdAsync(id);
        _movies.Remove(deletingMovie);
        await SaveDataAsync();

    }

    public async Task<List<Movie>> GetAllMoviesAsync()
    {
        var movieJson = await File.ReadAllTextAsync(_path);
        _movies = JsonSerializer.Deserialize<List<Movie>>(movieJson);
        return _movies;
    }

    public async Task<Movie> GetMovieByIdAsync(Guid id)
    {
        var movie =_movies.FirstOrDefault(x => x.Id == id);
        if (movie == null)
        {
            throw new Exception($" Movie with id:{id} not found");
        }
        return movie;
    }
    public async Task UpdateMovieAsync(Movie movie)
    {
        var movieFromDb = await GetMovieByIdAsync(movie.Id);
        var index = _movies.IndexOf(movieFromDb);
        _movies[index] = movie;
        await SaveDataAsync();

    }
    private async Task SaveDataAsync()
    {
        var movieJson = JsonSerializer.Serialize(_movies);
        await File.WriteAllTextAsync(_path, movieJson);
    }
}
