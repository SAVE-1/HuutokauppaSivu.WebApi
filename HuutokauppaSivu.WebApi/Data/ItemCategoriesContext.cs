using HuutokauppaSivu.WebApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HuutokauppaSivu.WebApi.Data;

public class ItemCategoriesContext : IdentityDbContext
{
    public DbSet<ItemCategories> ItemCategories { get; set; }

    public ItemCategoriesContext(DbContextOptions<ItemCategoriesContext> options) : base(options)
    {

    }
}
