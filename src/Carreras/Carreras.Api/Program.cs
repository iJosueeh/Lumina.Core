using Carreras.Application;
using Carreras.Infrastructure;
using DotNetEnv;
using MongoDB.Driver;
using Microsoft.Extensions.DependencyInjection;

Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var database = services.GetRequiredService<IMongoDatabase>();
    Carreras.Infrastructure.DataSeeder.Seed(database);
}

app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();