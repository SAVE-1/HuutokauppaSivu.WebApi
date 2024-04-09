using Huutokauppa_sivu.Server.Data;
using Huutokauppa_sivu.Server.Models;
using HuutokauppaSivu.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Huutokauppa_sivu.Server.Services;

public interface IItem
{
    public Task<List<MagicalItem>> GetAll(int skip, int take);
    public Task<MagicalItem> GetSingleFromDb(string id);
    public Task<List<MagicalItem>> GetPromotedItems(int take, int skip);
    public bool CreateNew(MagicalItem newItem);
    public MagicalItem Delete(string deleteIdentification);
    public string GetPostingCreator(string id);
    public Task<List<string>> GetCategoriesForSingleId(string id);
    public Task<List<CategoryLookup>> AreCategoriesValid(List<string> ids);
    public Task<Dictionary<int, string>> GetCategories(List<string> names);
    public Task InsertMultipleItemCategoryEntries(List<ItemCategories> entries);
}

public class MagicalItemsService : IItem
{
    private readonly MagicalItemsContext _context;

    public MagicalItemsService(MagicalItemsContext context)
    {
        _context = context;
    }

    public async Task<List<MagicalItem>> GetAll(int skip, int take)
    {
        var blog = _context.MagicalItems
            .Select(reg => reg)
            .OrderBy(reg => reg.Id)
            .Skip(skip)
            .Take(take);

        return await blog.ToListAsync();
    }

    public async Task<MagicalItem> GetSingleFromDb(string id)
    {
        var blog = await _context.MagicalItems.Where(b => b.DeleteIdentification == id).FirstAsync();

        return blog;
    }

    public async Task<List<MagicalItem>> GetPromotedItems(int skip, int take)
    {
        var blog = _context.MagicalItems
            .Where(b => b.IsPromoted);

        var blog2 = blog.OrderBy(reg => reg.Id)
            .Skip(skip)
            .Take(take);

        return await blog.ToListAsync<MagicalItem>();
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

    public async Task<List<string>> GetCategoriesForSingleId(string id)
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

        var categories = await query.Select(b => b.look.Name).ToListAsync();

        return categories;
    }

    /// <summary>
    /// Method <c>CheckIfExists</c> checks whether categories are valid.
    /// </summary>
    /// <returns>
    /// Returns categories that are valid, so returnValue.Length > 0 means there are valid categories.
    /// </returns>
    public async Task<List<CategoryLookup>> AreCategoriesValid(List<string> ids)
    {
        var l = await _context.CategoryLookup.Where(b => ids.Contains(b.Name)).Select(b => new { b.Name, b.Id }).ToListAsync();

        List<CategoryLookup> cats = new List<CategoryLookup>();

        foreach(var p in l)
        {
            cats.Add(new CategoryLookup { Name = p.Name, Id = p.Id });
        }

        return cats;
    }

    public async Task InsertMultipleItemCategoryEntries(List<ItemCategories> entries)
    {
        await _context.ItemCategories.AddRangeAsync(entries);
        _context.SaveChanges();
    }

    public async Task<Dictionary<int, string>> GetCategories(List<string> names)
    {
        var l = await _context.CategoryLookup.Where(b => names.Contains(b.Name)).Select(b => new { b.Name, b.Id }).ToListAsync();

        Dictionary<int, string> cats = new Dictionary<int, string>();

        foreach (var p in l)
        {
            cats.Add(p.Id, p.Name);
        }

        return cats;
    }

}
