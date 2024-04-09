using HuutokauppaSivu.WebApi.Controllers.Helpers;
using HuutokauppaSivu.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Huutokauppa_sivu.Server.Controllers;

public partial class MagicalItemsController : ControllerBase
{
    private List<CategoryLookup> GetInvalidCategories(List<CategoryLookup> cat, NewPostingHelper postItem)
    {
        return cat.Where(b => postItem.Categories.Contains(b.Name) == false).ToList();
    }

    private string CreateCategoryString(List<CategoryLookup> invalidCategories)
    {
        string invalidCategoriesString = "";

        foreach (CategoryLookup category in invalidCategories)
        {
            invalidCategoriesString = invalidCategoriesString + ", " + category.Name;
        }

        return invalidCategoriesString;
    }

    private List<string> CreateItemCategories(string id, List<string> categories)
    {

        Dictionary<int, string> cats = _myService.GetCategories(categories);

        if(categories.Count == cats.Count)
        {
            List<ItemCategories> entries = new List<ItemCategories>();
            foreach (var x in cats)
            {
                entries.Add(new ItemCategories
                {
                    DeleteIdentification = id,
                    CategoryId = x.Key
                });
            }

            _myService.InsertMultipleItemCategoryEntries(entries);
        }

        return new List<string>();
    }


}
