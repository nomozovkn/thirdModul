public interface IMovieService
{
    Task<Guid> AddMovieAsync(MovieDto movieDto);
    Task DeleteMovieAsync(Guid id);
    Task<List<MovieDto>> GetAllMoviesAsync();
    Task<List<MovieDto>> GetAllMoviesByDirectorAsync(string director);
    Task<MovieDto> GetTopRatedMovieAsync();
    Task<List<MovieDto>> GetMoviesReleasedAfterYearAsync(int year);
    Task<MovieDto> GetHighestGrossingMovieAsync();
    Task<List<MovieDto>> SearchMoviesByTitleAsync(string keyword);
    Task<List<MovieDto>> GetMoviesWithinDurationRangeAsync(int minMinutes, int maxMinutes);
    Task<long> GetTotalBoxOfficeEarningsByDirectorAsync(string director);
    Task<List<MovieDto>> GetMoviesSortedByRatingAsync();
    Task<List<MovieDto>> GetRecentMoviesAsync(int years);
    Task<MovieDto> GetMovieByIdAsync(Guid id); // Added missing method signature
    Task UpdateMovieAsync(MovieDto movieDto);
}
