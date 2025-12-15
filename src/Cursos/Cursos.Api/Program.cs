using Cursos.Application;
using Cursos.Infrastructure;
using Cursos.Api.Extensions;
using Cursos.Api.gRPC;

DotNetEnv.Env.TraversePath().Load();

var builder = WebApplication.CreateBuilder(args);

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:4200") // Angular app's origin
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials(); // Allow credentials (like cookies, authorization headers)
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddGrpc();
builder.WebHost.ConfigureKestrel((context, options) =>
{
    options.Configure(context.Configuration.GetSection("Kestrel"));
});

builder.Services.AddGrpc();

builder.Services.AddControllers();

builder.Services.AddApplication();
builder.Services.AddInfraestructure(builder.Configuration);

var app = builder.Build();

// Seed the database
/*using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var database = services.GetRequiredService<MongoDB.Driver.IMongoDatabase>();
    Cursos.Infrastructure.DataSeeder.Seed(database);
}*/

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler();

// Use CORS middleware
app.UseCors("AllowSpecificOrigin"); // Apply the CORS policy

app.MapControllers();
app.MapGrpcService<CursosGrpcService>();

app.Run();