using MovieCRUD.DataAccess.Entity;
using MovieCRUD.Repository.Services;
using MovieCRUD.Service.MovieDTO;

namespace MovieCRUD.Service.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<Guid> AddMovieAsync(MovieDto movieDto)
    {
        var movie = ConvertToEntity(movieDto);
        var res = await _movieRepository.AddMovieAsync(movie);
        return res;
    }

    public async Task DeleteMovieAsync(Guid id)
    {
        await _movieRepository.DeleteMovieAsync(id);
    }

    public async Task<List<MovieDto>> GetAllMoviesAsync()
    {
        var movies = await _movieRepository.GetAllMoviesAsync();
        var movieDto = await Task.WhenAll(movies.Select(mo => ConvertToDtoAsync(mo)));
        return movieDto.ToList();
    }

    public async Task<List<MovieDto>> GetAllMoviesByDirectorAsync(string director)
    {
        var res = await _movieRepository.GetAllMoviesAsync();
        var list = res.Where(movie => movie.Director == director).ToList();
        var resList = await Task.WhenAll(list.Select(mo => ConvertToDtoAsync(mo)).ToList());
        return resList.ToList();
    }

    public async Task<MovieDto> GetHighestGrossingMovieAsync()
    {
        var movieList = await _movieRepository.GetAllMoviesAsync();
        var movieDto = await Task.WhenAll(movieList.Select(mo => ConvertToDtoAsync(mo)).ToList());
        var max = movieDto.Max(mo => mo.BoxOfficeEarnings);
        var res = movieDto.FirstOrDefault(mo => mo.BoxOfficeEarnings == max);
        return res;
    }

    public async Task<List<MovieDto>> GetMoviesReleasedAfterYearAsync(int year)
    {
        var movies = await _movieRepository.GetAllMoviesAsync();
        var list = movies.Where(movie => movie.ReleaseDate.Year > year).ToList();
        var resList = await Task.WhenAll(list.Select(mo => ConvertToDtoAsync(mo)).ToList());
        return resList.ToList();
    }

    public async Task<List<MovieDto>> GetMoviesSortedByRatingAsync()
    {
        var movies = await _movieRepository.GetAllMoviesAsync();
        var moviesDto = await Task.WhenAll(movies.Select(mo => ConvertToDtoAsync(mo)).ToList());
        var sortedMovies = moviesDto.OrderByDescending(mo => mo.Rating).ToList();
        return sortedMovies;
    }

    public async Task<List<MovieDto>> GetMoviesWithinDurationRangeAsync(int minMinutes, int maxMinutes)
    {
        var movies = await _movieRepository.GetAllMoviesAsync();
        var filtrDurationMinutes = movies.Where(movie => movie.DurationMinutes >= minMinutes && movie.DurationMinutes <= maxMinutes).ToList();
        var movieDto = await Task.WhenAll(filtrDurationMinutes.Select(mo => ConvertToDtoAsync(mo)).ToList());
        return movieDto.ToList();
    }

    public async Task<List<MovieDto>> GetRecentMoviesAsync(int years)
    {
        var movies = await _movieRepository.GetAllMoviesAsync();
        var lastYearFilms = movies.Where(movie => movie.ReleaseDate >= DateTime.Now.AddYears(-years)).ToList();
        var movieDto = await Task.WhenAll(lastYearFilms.Select(mo => ConvertToDtoAsync(mo)).ToList());
        return movieDto.ToList();
    }

    public async Task<MovieDto> GetTopRatedMovieAsync()
    {
        var movies = await _movieRepository.GetAllMoviesAsync();
        var movieDto = await Task.WhenAll(movies.Select(mo => ConvertToDtoAsync(mo)).ToList());
        var maxRating = movieDto.Max(max => max.Rating);
        var res = movieDto.FirstOrDefault(movies => movies.Rating == maxRating);
        return res;
    }

    public async Task<long> GetTotalBoxOfficeEarningsByDirectorAsync(string director)
    {
        var res = await _movieRepository.GetAllMoviesAsync();
        var list = res.Where(movie => movie.Director == director).ToList();
        var resList = await Task.WhenAll(list.Select(mo => ConvertToDtoAsync(mo)).ToList());
        var total = resList.Sum(mo => mo.BoxOfficeEarnings);
        return total;
    }

    public async Task<List<MovieDto>> SearchMoviesByTitleAsync(string keyword)
    {
        var movies = await _movieRepository.GetAllMoviesAsync();
        var list = movies.Where(movie => movie.Title.Contains(keyword)).ToList();
        var resList = await Task.WhenAll(list.Select(mo => ConvertToDtoAsync(mo)).ToList());
        return resList.ToList();
    }

    public async Task UpdateMovieAsync(MovieDto movieDto)
    {
        var movie = ConvertToEntity(movieDto);
        await _movieRepository.UpdateMovieAsync(movie);
    }

    private Movie ConvertToEntity(MovieDto movieDto)
    {
        return new Movie()
        {
            Id = movieDto.Id ?? Guid.NewGuid(),
            Title = movieDto.Title,
            Director = movieDto.Director,
            DurationMinutes = movieDto.DurationMinutes,
            Rating = movieDto.Rating,
            BoxOfficeEarnings = movieDto.BoxOfficeEarnings,
            ReleaseDate = movieDto.ReleaseDate,
        };
    }

    private MovieDto ConvertToDtoAsync(Movie movie)
    {
        return new MovieDto()
        {
            Id = movie.Id,
            Title = movie.Title,
            Director = movie.Director,
            DurationMinutes = movie.DurationMinutes,
            Rating = movie.Rating,
            BoxOfficeEarnings = movie.BoxOfficeEarnings,
            ReleaseDate = movie.ReleaseDate,
        };
    }
}
