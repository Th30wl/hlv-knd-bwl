using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.PlatformAbstractions;
using System.Reflection;

namespace BowlingScoreCalculator.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var workingDir = PlatformServices.Default.Application.ApplicationBasePath;
        var assemblyName = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<Bowling.ICalculator, Bowling.Calculator>();
        builder.Services.AddSwaggerGen((o) =>
        {
            o.IncludeXmlComments(Path.Combine(workingDir, assemblyName + ".xml"));
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();

        app.Run();
    }
}