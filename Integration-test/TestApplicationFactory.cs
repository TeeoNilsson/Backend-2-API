using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class TestWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>
    where TProgram : class
{

    private readonly SqliteConnection sqliteConnection;

    public TestWebApplicationFactory()
    {
        sqliteConnection = new SqliteConnection("DataSource=:memory:");
        sqliteConnection.Open();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Radera alla existerande db-relaterade dependencies
            var dbDescriptors = services.Where(descriptor =>
                descriptor.ServiceType == typeof(DbContextOptions<AppDbContext>) ||
                descriptor.ServiceType == typeof(DbContextOptions) ||
                descriptor.ServiceType.ToString().Contains("EntityFrameworkCore") ||
                descriptor.ServiceType.ToString().Contains("Npgsql")
                ).ToList();

            foreach (var descriptor in dbDescriptors)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(sqliteConnection);
            });

            services.AddAuthentication("TestScheme")
                .AddScheme<AuthenticationSchemeOptions, FakeAuthenticationHandler>("TestScheme", options => { });

            var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                dbContext.Database.EnsureCreated();

                var user = new User();
                user.Id = "test-user-id";
                user.Email = "test-user";
                user.UserName = "test-user";

                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }
        });
    }
}

class FakeAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public FakeAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder) : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new[] {
            new Claim(ClaimTypes.NameIdentifier, "test-user-id"),
            new Claim(ClaimTypes.Name, "test-user"),
           // new Claim(ClaimTypes.Role, "admin"),
        };

        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "TestScheme");

        var result = AuthenticateResult.Success(ticket);
        return Task.FromResult(result);
    }
}