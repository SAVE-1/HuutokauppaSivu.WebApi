using Huutokauppa_sivu.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Huutokauppa_sivu.Server.Data;

public class MagigalItemsContext : DbContext
{
    public DbSet<MagicalItem> MagicalItemInventory { get; set; }

    public string DbPath { get; }

    public MagigalItemsContext()
    {
        var folder = Environment.CurrentDirectory;
        DbPath = System.IO.Path.Join(folder, "itemcatalog.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

}

