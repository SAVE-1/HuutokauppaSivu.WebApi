using Huutokauppa_sivu.Server.Data;
using Huutokauppa_sivu.Server.Models;
using System.Linq;

namespace Huutokauppa_sivu.Server.Services;

public interface IItem
{
    public List<MagicalItem> GetAll();
    public MagicalItem GetSingleFromDb(int id);
    public List<MagicalItem> GetPromotedItems();
    public bool CreateNew(MagicalItem newItem);
    public MagicalItem Delete(string deleteIdentification);
}

public class MagicalItemsService : IItem
{
    private readonly MagicalItemsContext _context;

    public MagicalItemsService(MagicalItemsContext context)
    {
        _context = context;
    }

    public List<MagicalItem> GetAll()
    {
        var blog = _context.MagicalItems.Select(reg => reg);

        return blog.ToList();
    }

    public MagicalItem GetSingleFromDb(int id)
    {
        var blog = _context.MagicalItems.Where(b => b.Id == id).First();

        return blog;
    }

    public List<MagicalItem> GetPromotedItems()
    {
        var blog = _context.MagicalItems.Where(b => b.IsPromoted);

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
}
