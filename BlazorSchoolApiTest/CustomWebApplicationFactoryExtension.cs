using BlazorSchoolApi;
using BlazorSchoolApi.Data;

namespace BlazorSchoolApiTest
{
    public static class CustomWebApplicationFactoryExtension
    {
        /// <summary>
        /// Use this method to seed extra data for a specific test class using this factory to resolve the Context
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="func">The function.</param>
        public static void SeedData(this CustomWebApplicationFactory<Program> factory, Action<SchoolContext> func)
        {
            using var scope = factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<SchoolContext>();
            func(db);

            db.SaveChanges();
        }
    }
}
