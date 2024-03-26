using Huutokauppa_sivu.Server.Data;
using Huutokauppa_sivu.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Models;

public static partial class SeedData
{

    private static void SeedMagicalItems(IServiceProvider serviceProvider)
    {
        using (var context = new MagicalItemsContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MagicalItemsContext>>()))
        {
            // Look for any movies.
            if (context.MagicalItems.Any())
            {
                return;   // DB has been seeded
            }

            context.MagicalItems.AddRange(
                new MagicalItem
                {
                    Name = "Divine Rapier",
                    Price = 6350,
                    Description = "The revered divine rapier",
                    IsPromoted = true,
                    PromotionImage = "rapier.jpg",
                    DeleteIdentification = GetHash("rapier"),
                    CreatedBy = "SYSTEM"
                },
                new MagicalItem
                {
                    Name = "AWP",
                    Price = 4750,
                    Description = "AWP",
                    IsPromoted = true,
                    PromotionImage = "awp.jpg",
                    DeleteIdentification = GetHash("awp"),
                    CreatedBy = "SYSTEM"
                },
                new MagicalItem
                {
                    Name = "Excalibur",
                    Price = 10000,
                    Description = "Excalibur",
                    IsPromoted = false,
                    PromotionImage = "excal.jpg",
                    DeleteIdentification = GetHash("excal"),
                    CreatedBy = "SYSTEM"
                },
                new MagicalItem
                {
                    Name = "Aghanim's Scepter",
                    Price = 4200,
                    Description = "Aghs",
                    IsPromoted = true,
                    PromotionImage = "aghs.jpg",
                    DeleteIdentification = GetHash("aghs"),
                    CreatedBy = "SYSTEM"
                }
            );
            context.SaveChanges();
        }
    }



}
