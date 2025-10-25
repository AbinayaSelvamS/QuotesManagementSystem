using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuotesManagement.Application.Interfaces;
using QuotesManagement.Application.Service;
using QuotesManagement.Data.DB;
using QuotesManagement.Data.IRepository;
using QuotesManagement.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContext with SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("QuotesManagement.Data")
    )
);

// Register service and repository
builder.Services.AddScoped<IQuotesService, QuotesService>();
builder.Services.AddScoped<IQuotesRepository, QuotesRepository>();
// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy
            .WithOrigins("http://localhost:4200", "https://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

// ✅ Ensure database exists, run migrations, and set WAL mode
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    try
    {
        // Apply migrations if needed
        db.Database.Migrate();

        // Set Write-Ahead Logging (improves concurrency)
        db.Database.ExecuteSqlRaw("PRAGMA journal_mode=WAL;");
    }
    catch (Microsoft.Data.Sqlite.SqliteException ex)
    {
        Console.WriteLine($"SQLite initialization failed: {ex.Message}");

        // Fallback if migrations fail (dev mode)
        db.Database.EnsureCreated();
    }
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//angular listener
app.UseCors("AllowAngular");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
