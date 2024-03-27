using Huutokauppa_sivu.Server.Data;
using HuutokauppaSivu.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Models;

public static partial class SeedData
{
    private static void SeedItemCategories(IServiceProvider serviceProvider)
    {
        using (var context = new MagicalItemsContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MagicalItemsContext>>()))
        {
            // Look for any movies.
            if (context.ItemCategories.Any())
            {
                return;   // DB has been seeded
            }

            context.ItemCategories.AddRange(
                new ItemCategories
                {
                    CategoryId = 1,
                    DeleteIdentification = generatedKeys["rapier"]
                },
                new ItemCategories
                {
                    CategoryId = 2,
                    DeleteIdentification = generatedKeys["awp"]
                },
                new ItemCategories
                {
                    CategoryId = 3,
                    DeleteIdentification = generatedKeys["rapier"]
                },
                new ItemCategories
                {
                    CategoryId = 4,
                    DeleteIdentification = generatedKeys["awp"]
                },
                new ItemCategories
                {
                    CategoryId = 5,
                    DeleteIdentification = generatedKeys["awp"]
                },
                new ItemCategories
                {
                    CategoryId = 6,
                    DeleteIdentification = generatedKeys["awp"]
                }
            );

            context.SaveChanges();
        }
    }
}
