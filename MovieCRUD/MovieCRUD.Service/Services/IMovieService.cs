using MovieCRUD.Service.MovieDTO;

namespace MovieCRUD.Service.Services;

public interface IMovieService
{
    Task<Guid>? AddMovieAsync(MovieDto movieDto);
    Task DeleteMovieAsync(Guid id);
    Task<List<MovieDto>> GetAllMoviesAsync();
    Task<List<MovieDto>> GetAllMoviesByDirectorAsync(string director);
    Task<MovieDto> GetTopRatedMovieAsync(); // ratingi eng baland film qaytarilsin
    Task<List<MovieDto>> GetMoviesReleasedAfterYearAsync(int year); // yilidan keyin chiqqan filmlar qaytarilsin
    Task<MovieDto> GetHighestGrossingMovieAsync(); // eng ko'p daromad qilgan film qaytarilsin
    Task<List<MovieDto>> SearchMoviesByTitleAsync(string keyword); // titleda keyword qatnashgan filmlar royxati qaytsin
    Task<List<MovieDto>> GetMoviesWithinDurationRangeAsync(int minMinutes, int maxMinutes); // davomiyligi min va max oralig'ida bo'lgan filmlar
    Task<long> GetTotalBoxOfficeEarningsByDirectorAsync(string director); // directorning filmlari qancha daromad qilgani qaytarilsin
    Task<List<MovieDto>> GetMoviesSortedByRatingAsync(); // baholanish bo'yicha sortlab bering. Kattadan kichikga
    Task<List<MovieDto>> GetRecentMoviesAsync(int years); // so'nggi yil ichida chiqqan filmlar royxati qaytarilsin. 
    Task UpdateMovieAsync(MovieDto movieDto);
}