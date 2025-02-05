using Microsoft.AspNetCore.Mvc;
using MovieCRUD.Service.MovieDTO;
using MovieCRUD.Service.Services;

namespace MovieCRUD.Server.Controllers
{
    [ApiController]
    [Route("api/Movie")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        [HttpPost("addMovieAsync")]
        public async Task<Guid> AddMovieAsync(MovieDto movieDto)
        {
          
            var resId= await _movieService.AddMovieAsync(movieDto);
            return resId;
        }
        [HttpDelete("deleteMovieAsync")]
        public async Task DeleteMovieAsync(Guid id)
        {
            await _movieService.DeleteMovieAsync(id);
        }
        [HttpGet("getAllMoviesAsync")]
        public async Task<List<MovieDto>> GetAllMoviesAsync()
        {
            return await _movieService.GetAllMoviesAsync();
        }
        [HttpGet("getAllMoviesByDirectorAsync")]
        public async Task<List<MovieDto>> GetAllMoviesByDirectorAsync(string director)
        {
            return await _movieService.GetAllMoviesByDirectorAsync(director);
        }
        [HttpGet("getTopRatedMovieAsync")]
        public async Task<MovieDto> GetTopRatedMovieAsync()
        {
            return await _movieService.GetTopRatedMovieAsync();
        }
        [HttpGet("getMoviesReleasedAfterYearAsync")]
        public async Task<List<MovieDto>> GetMoviesReleasedAfterYearAsync(int year)
        {
            return await _movieService.GetMoviesReleasedAfterYearAsync(year);
        }
        [HttpGet("getHighestGrossingMovieAsync")]
        public async Task<MovieDto> GetHighestGrossingMovieAsync()
        {
            return await _movieService.GetHighestGrossingMovieAsync();
        }
        [HttpGet("searchMoviesByTitleAsync")]
        public async Task<List<MovieDto>> SearchMoviesByTitleAsync(string keyword)
        {
            return await _movieService.SearchMoviesByTitleAsync(keyword);
        }
        [HttpGet("getMoviesWithinDurationRangeAsync")]
        public async Task<List<MovieDto>> GetMoviesWithinDurationRangeAsync(int minMinutes, int maxMinutes)
        {
            return await _movieService.GetMoviesWithinDurationRangeAsync(minMinutes, maxMinutes);
        }
        [HttpGet("getTotalBoxOfficeEarningsByDirectorAsync")]
        public async Task<long> GetTotalBoxOfficeEarningsByDirectorAsync(string director)
        {
            return await _movieService.GetTotalBoxOfficeEarningsByDirectorAsync(director);
        }
        [HttpGet("getMoviesSortedByRatingAsync")]
        public async Task<List<MovieDto>> GetMoviesSortedByRatingAsync()
        {
            return await _movieService.GetMoviesSortedByRatingAsync();
        }
        [HttpGet("getRecentMoviesAsync")]
        public async Task<List<MovieDto>> GetRecentMoviesAsync(int years)
        {
            return await _movieService.GetRecentMoviesAsync(years);
        }
        //[HttpGet("getMovieByIdAsync")]
        //public async Task<MovieDto> GetMovieByIdAsync(Guid id)
        //{
        //    return await _movieService.GetMovieByIdAsync(id);
        //}
        [HttpGet(" updateMovieAsync")]
        public async Task UpdateMovieAsync(MovieDto movieDto)
        {
             await _movieService.UpdateMovieAsync(movieDto);
        }



    }
}
