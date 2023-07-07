using Microsoft.EntityFrameworkCore;
using NZWalksApi.DataAcessLayer.Data;
using NZWalksApi.DataAcessLayer.Mappers;
using NZWalksApi.DataAcessLayer.Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();  
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Injecting Db Context by using dependancy injection
        builder.Services.AddDbContext<NZWalksDbContext> (options => options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalks")));

        // Injecting Repository interface by using Dependancy injection
        builder.Services.AddScoped<IRegionRepository, DbRegionRepository>();

        // Injectinh Automapper file "AutoMapperProfile" by using Dependancy Injection
        builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

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