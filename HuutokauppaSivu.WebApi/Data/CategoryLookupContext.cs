using HuutokauppaSivu.WebApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HuutokauppaSivu.WebApi.Data;

public class CategoryLookupContext : IdentityDbContext
{
    public DbSet<CategoryLookup> MagicalItems { get; set; }

    public CategoryLookupContext(DbContextOptions<CategoryLookupContext> options) : base(options)
    {

    }
}
