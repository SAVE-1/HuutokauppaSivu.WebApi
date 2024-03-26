using Huutokauppa_sivu.Server.Data;
using HuutokauppaSivu.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Models;

public static partial class SeedData
{
    private static void SeedCategoryLookup(IServiceProvider serviceProvider)
    {
        using (var context = new MagicalItemsContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MagicalItemsContext>>()))
        {
            // Look for any movies.
            if (context.CategoryLookup.Any())
            {
                return;   // DB has been seeded
            }

            context.CategoryLookup.AddRange(
                new CategoryLookup
                {
                    CategoryId = 1,
                    Name = "sword"
                },
                new CategoryLookup
                {
                    CategoryId = 2,
                    Name = "gun"
                },
                new CategoryLookup
                {
                    CategoryId = 3,
                    Name = "extremely important"
                },
                new CategoryLookup
                {
                    CategoryId = 4,
                    Name = "legendary"
                },
                new CategoryLookup
                {
                    CategoryId = 5,
                    Name = "mechanical"
                },
                new CategoryLookup
                {
                    CategoryId = 6,
                    Name = "cs2"
                }
            );

            context.SaveChanges();
        }
    }
}
