using MovieCRUD.DataAccess.Entity;
using MovieCRUD.Repository.Services;
using MovieCRUD.Service.MovieDTO;

namespace MovieCRUD.Service.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    public MovieService()
    {
        _movieRepository = new MovieRepository();
    }
    public async Task<Guid> AddMovieAsync(MovieDto movieDto)
    {
        var movie = await ConverToEntityAsync(movieDto);
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
        var movieDto = movies.Select(async mo => await ConvertToDtoAsync(mo)).ToList();
        var movieDtoTasks = await Task.WhenAll(movieDto);
        return movieDtoTasks.ToList();
    }

    public async Task<List<MovieDto>> GetAllMoviesByDirectorAsync(string director)
    {
        var res = await _movieRepository.GetAllMoviesAsync();
        var list = res.Where(movie => movie.Director == director).ToList();
        var resList = await Task.WhenAll(list.Select(async mo => await ConvertToDtoAsync(mo)).ToList());
        return resList.ToList();

    }

    public async Task<MovieDto> GetHighestGrossingMovieAsync() // eng ko'p daromad qlgan film
    {
        var movieList = await _movieRepository.GetAllMoviesAsync();
        var movieDto = await Task.WhenAll(movieList.Select(async mo => await ConvertToDtoAsync(mo)).ToList());
        var max = movieDto.Max(mo => mo.BoxOfficeEarnings);
        var res = movieDto.FirstOrDefault(mo => mo.BoxOfficeEarnings == max);
        return res;
    }

   

    public async Task<List<MovieDto>> GetMoviesReleasedAfterYearAsync(int year) // yildan keyin ciqqan film
    {
        var movies = await _movieRepository.GetAllMoviesAsync();
        var list = movies.Where(movie => movie.ReleaseDate.Year > year).ToList();
        var resList = await Task.WhenAll(list.Select(async mo => await ConvertToDtoAsync(mo)).ToList());
        return resList.ToList();
    }

    public async Task<List<MovieDto>> GetMoviesSortedByRatingAsync() //Reyting bn sortlash kattadan kicikka
    {
        var movies = await _movieRepository.GetAllMoviesAsync();
        var moviesDto = await Task.WhenAll(movies.Select(async mo => await ConvertToDtoAsync(mo)).ToList());
        var sortedMovies = moviesDto.OrderByDescending(mo => mo.Rating).ToList();
        return sortedMovies;
    }

    public async Task<List<MovieDto>> GetMoviesWithinDurationRangeAsync(int minMinutes, int maxMinutes) // davomiyligi min va max orasida
    {
        var movies = await _movieRepository.GetAllMoviesAsync();
        var filtrDurationMinutes = movies.Where(movie => movie.DurationMinutes >= minMinutes && movie.DurationMinutes <= maxMinutes).ToList();
        var movieDto = await Task.WhenAll(filtrDurationMinutes.Select(async mo => await ConvertToDtoAsync(mo)).ToList());
        return movieDto.ToList(); ;
    }

    public async Task<List<MovieDto>> GetRecentMoviesAsync(int years) // so'ngi yil icida chiqarilgan film
    {
        var movies = await _movieRepository.GetAllMoviesAsync();
        var lastYearFilms = movies.Where(movie => movie.ReleaseDate.Year == years).ToList();
        var movieDto = await Task.WhenAll(lastYearFilms.Select(async mo => await ConvertToDtoAsync(mo)).ToList());
        return movieDto.ToList(); ;
    }

    public async Task<MovieDto> GetTopRatedMovieAsync()// reytingi eng balant film 
    {
        var movies = await _movieRepository.GetAllMoviesAsync();
        var movieDto = await Task.WhenAll(movies.Select(mo=>ConvertToDtoAsync(mo)).ToList());
        var maxRating = movieDto.Max(max => max.Rating);
        var res=movieDto.FirstOrDefault(movies => movies.Rating == maxRating);
        return res;


    }

    public async Task<long> GetTotalBoxOfficeEarningsByDirectorAsync(string director)//directorni filmlari qancha daromad qilgani
    {
        var res = await _movieRepository.GetAllMoviesAsync();
        var list = res.Where(movie => movie.Director == director).ToList();
        var resList = await Task.WhenAll(list.Select(async mo => await ConvertToDtoAsync(mo)).ToList());
        var total = resList.Sum(mo => mo.BoxOfficeEarnings);
        return total;
    }

    public async Task<List<MovieDto>> SearchMoviesByTitleAsync(string keyword)// title da keyword qatnashgan film qaytarilsin
    {
        var movies = await _movieRepository.GetAllMoviesAsync();
        var list = movies.Where(movie => movie.Title.Contains(keyword)).ToList();
        var resList = await Task.WhenAll(list.Select(async mo => await ConvertToDtoAsync(mo)).ToList());
        return resList.ToList();
    }

    public async Task UpdateMovieAsync(MovieDto movieDto)
    {
        var movie = await ConverToEntityAsync(movieDto);
        await _movieRepository.UpdateMovieAsync(movie);
    }
   
    
    private async Task<Movie> ConverToEntityAsync(MovieDto movieDto)
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

    private async Task<MovieDto> ConvertToDtoAsync(Movie movie)
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
