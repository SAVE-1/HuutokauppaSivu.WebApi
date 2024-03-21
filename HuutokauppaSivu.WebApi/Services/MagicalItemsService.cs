using Huutokauppa_sivu.Server.Data;
using Huutokauppa_sivu.Server.Models;

namespace Huutokauppa_sivu.Server.Services;

public interface IItem
{
    public List<MagicalItem> GetAll(int take, int skip);
    public MagicalItem GetSingleFromDb(int id);
    public List<MagicalItem> GetPromotedItems(int skip, int take);
    public bool CreateNew(MagicalItem newItem);
    public MagicalItem Delete(string deleteIdentification);
    public string GetPostingCreator(string id);
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
}
