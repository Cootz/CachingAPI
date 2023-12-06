using CachingAPI.Implementation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<CachingApiDbContext>(
        options => options.UseNpgsql(builder.Configuration.GetConnectionString("CachingApiDatabase")))
    .AddControllers();

var app = builder.Build();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<CachingApiDbContext>();
    context.Database.Migrate();
}

app.MapControllers();

app.Run();