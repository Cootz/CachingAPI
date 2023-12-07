using CachingAPI.Implementation.Clients;
using CachingAPI.Implementations.Db;
using CachingAPI.Implementations.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpClient()
    .AddScoped<JsonPlaceholderClient>()
    .AddScoped<DbCacheProvider>()
    .AddScoped<UsersProvider>()
    .AddScoped<AlbumsProvider>()
    .AddSwaggerGen()
    .AddDbContext<CachingApiDbContext>(
        options => options.UseNpgsql(builder.Configuration.GetConnectionString("CachingApiDatabase")))
    .AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<CachingApiDbContext>();
    context.Database.Migrate();
}

app.MapControllers();

app.Run();