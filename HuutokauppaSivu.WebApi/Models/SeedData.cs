using Huutokauppa_sivu.Server.Data;
using Huutokauppa_sivu.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MagigalItemsContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MagigalItemsContext>>()))
        {
            // Look for any movies.
            if (context.MagicalItem.Any())
            {
                return;   // DB has been seeded
            }
            context.MagicalItem.AddRange(
                new MagicalItem
                {
                    Name = "Divine Rapier",
                    Price = 6350,
                    Description = "The revered divine rapier",
                    IsPromoted = true,
                    PromotionImage = "rapier.jpg"
                },
                new MagicalItem
                {
                    Name = "AWP",
                    Price = 4750,
                    Description = "AWP",
                    IsPromoted = true,
                    PromotionImage = "awp.jpg"
                },
                new MagicalItem
                {
                    Name = "Excalibur",
                    Price = 10000,
                    Description = "Excalibur",
                    IsPromoted = false,
                    PromotionImage = "excal.jpg"
                },
                new MagicalItem
                {
                    Name = "Aghanim's Scepter",
                    Price = 4200,
                    Description = "Aghs",
                    IsPromoted = true,
                    PromotionImage = "aghs.jpg"
                }
            );
            context.SaveChanges();
        }
    }
}
