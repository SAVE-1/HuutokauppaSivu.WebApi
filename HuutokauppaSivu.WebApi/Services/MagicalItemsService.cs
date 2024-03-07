using Huutokauppa_sivu.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace Huutokauppa_sivu.Server.Services;

public static class MagicalItemsService
{
    public static DbSet<MagicalItem> MagicalItems { get; set; }

    public static string DbPath { get; }

    static List<MagicalItem> MagicalItems_ { get; }

    static MagicalItemsService()
    {
        MagicalItems_ = new List<MagicalItem>
        {
            new MagicalItem { Id = 1, Price = 100, Name = "Excalibur" },
            new MagicalItem { Id = 2, Price = 100, Name = "Vorpal sword" }
        };
    }

    public static List<MagicalItem> GetAll() => MagicalItems_;

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
