using Huutokauppa_sivu.Server.Models;
using HuutokauppaSivu.WebApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Huutokauppa_sivu.Server.Data;

public class MagicalItemsContext : IdentityDbContext
{
    public DbSet<MagicalItem> MagicalItems { get; set; }
    public DbSet<ItemCategories> ItemCategories { get; set; }
    public DbSet<CategoryLookup> CategoryLookup { get; set; }

    public MagicalItemsContext(DbContextOptions<MagicalItemsContext> options) : base(options)
    {

    }
}

