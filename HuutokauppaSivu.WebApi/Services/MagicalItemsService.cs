using Huutokauppa_sivu.Server.Data;
using Huutokauppa_sivu.Server.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Huutokauppa_sivu.Server.Services;

public static class MagicalItemsService
{
    public static string DbPath { get; }

    static MagicalItemsService()
    {

    }

    public static List<MagicalItem> GetAll()
    {
        using var db = new MagigalItemsContext();

        var blog = db.MagicalItem.Select(reg => reg);

        return blog.ToList();

    }

    public static List<MagicalItem> GetSingleFromDb(int id)
    {
        using var db = new MagigalItemsContext();

        var blog = db.MagicalItem.OrderBy(b => b.Id == id).First();

        return new List<MagicalItem> { blog };
    }

    public static List<MagicalItem> GetPromotedItems()
    {
        using var db = new MagigalItemsContext();

        var blog = db.MagicalItem.Where(b => b.IsPromoted);

        return blog.ToList<MagicalItem>();
    }

    //public BloggingContext()
    //{
    //    var folder = Environment.SpecialFolder.LocalApplicationData;
    //    var path = Environment.GetFolderPath(folder);
    //    DbPath = System.IO.Path.Join(path, "blogging.db");
    //}

    //// The following configures EF to create a Sqlite database file in the
    //// special "local" folder for your platform.
    //protected override void OnConfiguring(DbContextOptionsBuilder options)
    //    => options.UseSqlite($"Data Source={DbPath}");



}
