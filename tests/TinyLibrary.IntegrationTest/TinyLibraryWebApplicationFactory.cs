using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Time.Testing;
using TinyLibrary.Infrastructure.Persistence;

namespace TinyLibrary.IntegrationTest;

public class TinyLibraryWebApplicationFactory : WebApplicationFactory<Program>
{

    private readonly SqliteConnection connection = new("DataSource=:memory:");

    public FakeTimeProvider TimeProvider { get; } = new(DateTimeOffset.UtcNow);


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        connection.Open();

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<LibraryDbContext>>();
            services.AddDbContext<LibraryDbContext>(options => options.UseSqlite(connection));

            services.RemoveAll<TimeProvider>();
            services.AddSingleton<TimeProvider>(TimeProvider);
        });
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        connection.Dispose();
    }
}
