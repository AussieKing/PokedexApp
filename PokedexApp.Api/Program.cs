using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PokedexApp.Data;
using PokedexApp.Middlewares;
using PokedexApp.Repositories;
using PokedexApp.Services;
using PokedexApp.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddTransient<IPokemonRepository, PokemonRepository>();
builder.Services.AddTransient<IPokemonService, PokemonService>();
builder
    .Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PokemonValidator>());

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AllowReactApp",
        builder =>
        {
            builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
        }
    );
});

// Configure EF Core with SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
Console.WriteLine($"Using connection string: {connectionString}");
builder.Services.AddDbContext<PokedexContext>(options =>
    options.UseSqlServer(
        connectionString,
        sqlOptions =>
        {
            sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null
            );
        }
    )
);

// Memory Cache
builder.Services.AddMemoryCache();

var app = builder.Build();

// Apply the CORS policy before other middlewares
app.UseCors("AllowReactApp");

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PokedexApp v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
