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
                    MagicalItemId = 1,
                },
                new ItemCategories
                {
                    CategoryId = 2,
                    MagicalItemId = 2
                },
                new ItemCategories
                {
                    CategoryId = 3,
                    MagicalItemId = 3
                },
                new ItemCategories
                {
                    CategoryId = 4,
                    MagicalItemId = 4
                },
                new ItemCategories
                {
                    CategoryId = 5,
                    MagicalItemId = 4
                },
                new ItemCategories
                {
                    CategoryId = 6,
                    MagicalItemId = 4
                }
            );

            context.SaveChanges();
        }
    }
}
