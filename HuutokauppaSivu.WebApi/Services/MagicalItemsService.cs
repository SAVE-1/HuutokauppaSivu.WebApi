﻿using Huutokauppa_sivu.Server.Data;
using Huutokauppa_sivu.Server.Models;
using HuutokauppaSivu.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using EFCore.BulkExtensions;
using System.Collections.Generic;

namespace Huutokauppa_sivu.Server.Services;

public interface IItem
{
    public List<MagicalItem> GetAll(int take, int skip);
    public MagicalItem GetSingleFromDb(int id);
    public List<MagicalItem> GetPromotedItems(int skip, int take);
    public bool CreateNew(MagicalItem newItem);
    public MagicalItem Delete(string deleteIdentification);
    public string GetPostingCreator(string id);
    public List<string> GetCategoriesForSingleId(string id);
    public List<CategoryLookup> AreCategoriesValid(List<string> ids);
}

public class MagicalItemsService : IItem
{
    private readonly MagicalItemsContext _context;

    public MagicalItemsService(MagicalItemsContext context)
    {
        _context = context;
    }

    public List<MagicalItem> GetAll(int skip, int take)
    {
        var blog = _context.MagicalItems
            .Select(reg => reg)
            .OrderBy(reg => reg.Id)
            .Skip(skip)
            .Take(take);

        return blog.ToList();
    }

    public MagicalItem GetSingleFromDb(int id)
    {
        var blog = _context.MagicalItems.Where(b => b.Id == id).First();

        return blog;
    }

    public List<MagicalItem> GetPromotedItems(int skip, int take)
    {
        var blog = _context.MagicalItems
            .Where(b => b.IsPromoted)
            .OrderBy(reg => reg.Id)
            .Skip(skip)
            .Take(take);

        return blog.ToList<MagicalItem>();
    }

    public bool CreateNew(MagicalItem newItem)
    {
        var blog = _context.MagicalItems.Add(newItem);

        _context.SaveChanges();

        return true;
    }

    public MagicalItem Delete(string deleteIdentification)
    {
        var blog = _context.MagicalItems.First(p => p.DeleteIdentification == deleteIdentification);
        _context.Remove(blog);
        _context.SaveChanges();

        return blog;
    }

    public string GetPostingCreator(string id)
    {
        var col = _context.MagicalItems
                           .Where(b => b.DeleteIdentification == id);

        if (col.Any())
        {
            MagicalItem item = col.First();
            return item.CreatedBy!;
        }

        return "";
    }

    public List<string> GetCategoriesForSingleId(string id)
    {
        /*
            -- ORIGINAL SQL QUERY --
            -- Get categories
            SELECT item.[Id]
            ,item.[Price]
            ,item.[Name]
	        ,look.[Name] as Category
            FROM [HuutokauppaSivu].[dbo].[MagicalItems]       as item
            INNER JOIN [HuutokauppaSivu].[dbo].ItemCategories as cat  ON(cat.MagicalItemId = item.Id)
            INNER JOIN [HuutokauppaSivu].[dbo].CategoryLookup as look ON(cat.CategoryId = look.CategoryId)
            -- WHERE item.name = 'AWP'
            WHERE item.DeleteIdentification = 'EAC1A457CAD7092A1E6E9D1322BC5019'
        */

        // comments query in LINQ, https://learn.microsoft.com/en-us/ef/core/querying/complex-query-operators
        var query = from item in _context.Set<MagicalItem>().Where(b => b.DeleteIdentification == id)
                    join cat in _context.Set<ItemCategories>()
                        on item.DeleteIdentification equals cat.DeleteIdentification
                    join look in _context.Set<CategoryLookup>()
                        on cat.CategoryId equals look.CategoryId
                    select new { look };

        var categories = query.Select(b => b.look.Name).ToList();

        return categories;
    }

    /// <summary>
    /// Method <c>CheckIfExists</c> checks whether categories are valid.
    /// </summary>
    /// <returns>
    /// Returns categories that are valid, so returnValue.Length > 0 means there are valid categories.
    /// </returns>
    public List<CategoryLookup> AreCategoriesValid(List<string> ids)
    {
        var l = _context.CategoryLookup.Where(b => ids.Contains(b.Name)).Select(b => new { b.Name, b.Id }).ToList();

        List<CategoryLookup> cats = new List<CategoryLookup>();

        foreach(var p in l)
        {
            cats.Add(new CategoryLookup { Name = p.Name, Id = p.Id });
        }

        return cats;
    }

    public void InsertCategoryLookupEntries(List<ItemCategories> entries)
    {
        _context.ItemCategories.AddRange(entries);
    }

    public List<CategoryLookup> GetCategories(List<string> names) 
    {

        return new List<CategoryLookup>();


    }

}
