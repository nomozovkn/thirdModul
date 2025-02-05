
using WebFile.Service.Services;
using WebFile.StorageBrokker.Services;

namespace WebFile.Server
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
            builder.Services.AddScoped<IStorageService, StorageService>();
            builder.Services.AddSingleton<IStorageBrokkerService, LocalStorageBrokkerService>();
            //builder.Services.AddSingleton<IStorageService, StorageService>();
            //builder.Services.AddTransient<IStorageService, StorageService>();
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
