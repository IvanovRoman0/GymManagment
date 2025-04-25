using GymManagement.Infrastructure.DbContexts;
using GymManagement.Infrastructure.Repositories;
using GymManagement.Services.Implementations;
using GymManagement.Services.Interfaces;
using GymManagement.Services.Mapping;
using GymManagment.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddDbContext<GymDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("GymDbContext"),
        npgsqlOptions => npgsqlOptions.EnableRetryOnFailure()));

builder.Services.AddAutoMapper(typeof(ClientProfile).Assembly);

builder.Services.Scan(scan => scan
    .FromAssemblies(typeof(ClientRepository).Assembly)
    .AddClasses(classes => classes.Where(t => t.Name.EndsWith("Repository")))
    .AsImplementedInterfaces()
    .WithScopedLifetime());
builder.Services.Scan(scan => scan
    .FromAssemblies(typeof(ClientService).Assembly)
    .AddClasses(classes => classes.Where(t => t.Name.EndsWith("Service")))
    .AsImplementedInterfaces()
    .WithScopedLifetime());


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GymManagement API",
        Version = "v1",
        Description = "API для сопртивного зала"
    });
    c.MapType<DateTime>(() => new OpenApiSchema { Type = "string", Format = "date-time" });
    c.MapType<DateTime?>(() => new OpenApiSchema { Type = "string", Format = "date-time", Nullable = true });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GymManagement API v1");
        c.DisplayRequestDuration();
        c.EnableDeepLinking();
    });
}
await app.MigrateDatabaseAsync();
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler("/ошибка");
app.Map("/ошибка", () => Results.Problem("произошла ошибка."));

app.Run();
public static class WebApplicationExtensions
{
    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<GymDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}