using GymManagement.Core.Entities;
using GymManagement.Infrastructure.DbContexts;
using GymManagement.Infrastructure.Repositories;
using GymManagement.Services;
using GymManagement.Services.Implementations;
using GymManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
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
builder.Services.AddAutoMapper(typeof(ClientService), typeof(MembershipService));
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IMembershipRepository, MembershipRepository>();
builder.Services.AddScoped<IMembershipService, MembershipService>();

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