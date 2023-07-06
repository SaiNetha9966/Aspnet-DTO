using Microsoft.EntityFrameworkCore;
using NZWalksApi.DataAcessLayer.Data;
using NZWalksApi.DataAcessLayer.Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();  
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Injecting Db Context by using dependancy injection
        builder.Services.AddDbContext<NZWalksDbContext> (options => options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalks")));

        // Injecting Repository interface by using Dependancy injection

        builder.Services.AddScoped<IRegionRepository, DbRegionRepository>();

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