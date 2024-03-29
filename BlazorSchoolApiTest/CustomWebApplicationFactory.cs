using BlazorSchoolApi.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;

namespace BlazorSchoolApiTest
{
    public class CustomWebApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the app's ApplicationDbContext registration.
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(DbContextOptions<SchoolContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

              
                services.AddDbContext<SchoolContext>(options =>
                {
                    options.UseSqlite("Data Source=School.db");
                });
                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<SchoolContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();
                    
                    db.Database.EnsureCreated();
                    //db.Database.Migrate();
                }
            });
        }

        public HttpClient CreateAuthenticatedClient()
        {
            var client = WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        services.AddAuthentication("Test")
                            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                                "Test", options => { });
                    });
                })
                .CreateClient(new WebApplicationFactoryClientOptions
                {
                    AllowAutoRedirect = false,
                });

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Test");
            return client;
        }
    }
}
