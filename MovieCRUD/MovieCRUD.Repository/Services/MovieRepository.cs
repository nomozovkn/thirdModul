using MovieCRUD.DataAccess.Entity;
using System.Text.Json;

namespace MovieCRUD.Repository.Services;

public class MovieRepository : IMovieRepository
{
    private readonly string _filepath;
    private readonly string _directoryPath;
    private List<Movie> _movies;
    public MovieRepository()
    {
        _filepath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "Movies.json");
        _directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");

        if (!Directory.Exists(_directoryPath))
        {
            Directory.CreateDirectory(_directoryPath);
        }
        if (!File.Exists(_filepath))
        {
            File.WriteAllText(_filepath, "[]");
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
        var movieJson = await File.ReadAllTextAsync(_filepath);
        _movies = JsonSerializer.Deserialize<List<Movie>>(movieJson);
        return _movies;
    }

    public async Task<Movie> GetMovieByIdAsync(Guid id)
    {
        var movie = _movies.FirstOrDefault(x => x.Id == id);
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
        await File.WriteAllTextAsync(_filepath, movieJson);
    }
}
