using Huutokauppa_sivu.Server.Models;
using Huutokauppa_sivu.Server.Services;
using HuutokauppaSivu.WebApi.Controllers.Helpers;
using HuutokauppaSivu.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Huutokauppa_sivu.Server.Controllers;

[ApiController]
[Route("[controller]")]
public partial class MagicalItemsController : ControllerBase
{
    private readonly IItem _myService;

    public MagicalItemsController(IItem myService)
    {
        _myService = myService;
    }

    // GET all action
    [HttpGet("")]
    public async Task<IActionResult> GetAll(int skip = 0, int take = 50)
    {

        try
        {
            List<MagicalItem> all = await _myService.GetAll(skip, take);
            return Ok(all);
        }
        catch
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }

    // GET all action
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingleById(string id)
    {
        return Ok(await _myService.GetSingleFromDb(id));
    }

    // GET all action
    [HttpGet("PromotedItems")]
    public async Task<IActionResult> GetPromoted(int skip = 0, int take = 50)
    {
        return Ok(await _myService.GetPromotedItems(skip, take));
    }

    // POST action
    [Authorize]
    [HttpPost("NewPosting")]
    public async Task<IActionResult> Create([FromForm] NewPostingHelper postItem)
    {  
        try
        { 
            if (postItem.Categories.Count > 50)
            {
                return BadRequest("Too many categories in request");
            }

            List<CategoryLookup> validCategories = await _myService.AreCategoriesValid(postItem.Categories);

            if (validCategories.Count != postItem.Categories.Count)
            {
                List<CategoryLookup> invalidCategories = GetInvalidCategories(validCategories, postItem);

                string invalidCategoriesString = CreateCategoryString(invalidCategories);

                return BadRequest($"Invalid categories in request: {invalidCategoriesString}");
            }

            // create a secondary ID, in order not to reveal the internal structure or additional info of the database to the browser
            string deleteIdentification = DataHelper.GetHash(postItem.Name);

            MagicalItem item = new MagicalItem()
            {
                Price = postItem.Price,
                Name = postItem.Name,
                Description = postItem.Description,
                IsPromoted = false,
                PromotionImage = null,
                DeleteIdentification = deleteIdentification,
                CreatedBy = User.FindFirstValue(ClaimTypes.Name)
            };

            bool createNewResult = _myService.CreateNew(item);

            List<string> createdCategories = await CreateItemCategoriesAsync(deleteIdentification, postItem.Categories);

            List<ItemCategories> itemCategories = new List<ItemCategories>();

            foreach (string category in postItem.Categories)
            {
                itemCategories.Add(new ItemCategories()
                {
                    DeleteIdentification = deleteIdentification,
                    CategoryId = 1
                });
            }

            if (createNewResult)
            {
                return Created("Created", item);
            }

            return Problem();
        }
        catch (Exception ex)
        {
            return Problem("VIRHE");
        }
    }

    [Authorize]
    [HttpDelete("{deleteIdentification}")]
    public async Task<IActionResult> Delete(string deleteIdentification)
    {
        string creator = _myService.GetPostingCreator(deleteIdentification);
        string currentUser = User.FindFirstValue(ClaimTypes.Name);

        if (creator == currentUser)
        {
            MagicalItem item = _myService.Delete(deleteIdentification);

            if (item != null)
            {
                return Ok(item);
            }

            return Problem();
        }

        return Unauthorized();
    }

    [HttpGet("{id}/GetCategories")]
    public async Task<IActionResult> GetCategoriesByIdSingle(string id)
    {
        var categories = await _myService.GetCategoriesForSingleId(id);

        return Ok(categories);
    }

}
