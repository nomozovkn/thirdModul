
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using System.Collections.Generic;
using System.IO;
using MovieCRUD.Service.Services;
using MovieCRUD.Repository.Services;

namespace MovieCRUD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<IMovieRepository, MovieRepository>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

//Qilinishi kk bo'lgan ishlar:
//* Githubga joylansin. Gitignore bo'lsin shart
//* Hamma ma'lumotlar json filega saqlansin
//* Service va Repository structuradan fo'ydalaning
//* Dto lar ishlatilsin
//* CRUD
//* List<MovieDto> GetAllMoviesByDirector(string director);
//*MovieDto GetTopRatedMovie(); // ratingi eng baland film qaytarilsin
//*List < MovieDto > GetMoviesReleasedAfterYear(int year); // yilidan keyin chiqqan filmlar qaytarilsin
//*MovieDto GetHighestGrossingMovie(); // eng ko'p daromad qilgan film qaytarilsin
//*List < MovieDto > SearchMoviesByTitle(string keyword); // titleda keyword qatnashgan filmlar royxati qaytsin
//*List < MovieDto > GetMoviesWithinDurationRange(int minMinutes, int maxMinutes); // davomiyligi min va max oralig'ida bo'lgan filmlar
//*long GetTotalBoxOfficeEarningsByDirector(string director); // directorning filmlari qancha daromad qilgani qaytarilsin
//*List < MovieDto > GetMoviesSortedByRating(); // baholanish bo'yicha sortlab bering. Kattadan kichikga
//*List < MovieDto > GetRecentMovies(int years); // so'nggi yil ichida chiqqan filmlar royxati qaytarilsin.

//*Service bo'limda MovieDto uchun quyidagi extension methodlarni yozing alohida extensions degan folderni ichida bo'lsin.
//* MovieDto uchun DurationMinutes ni soatga o'tkazib qaytaradigan
//* List<MovieDto> uchun listdagi barcha movielarni BoxOfficeEarnings larini yeg'indisini qaytaradigan
