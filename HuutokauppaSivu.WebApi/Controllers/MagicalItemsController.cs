using Huutokauppa_sivu.Server.Models;
using Huutokauppa_sivu.Server.Services;
using HuutokauppaSivu.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

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
        List<MagicalItem> all = _myService.GetAll(skip, take);

        if(all.Any())
        {
            return Ok(all);
        }

        return new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }

    // GET all action
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSingleById(string id)
    {
        return Ok(_myService.GetSingleFromDb(id));
    }

    // GET all action
    [HttpGet("PromotedItems")]
    public async Task<IActionResult> GetPromoted(int skip = 0, int take = 50)
    {
        return Ok(_myService.GetPromotedItems(skip, take));
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

            List<CategoryLookup> validCategories = _myService.AreCategoriesValid(postItem.Categories);

            if (validCategories.Count != postItem.Categories.Count)
    {

            return BadRequest($"Invalid categories in request: {invalidCategoriesString}");
        }
        MD5 sum = MD5.Create();

            string preHash = DataHelper.GetHash(postItem.Name);

        // create a secondary ID, in order not to reveal the internal structure or additional info of the database to the browser
        string deleteIdentification = BitConverter.ToString(sum.ComputeHash(Encoding.ASCII.GetBytes(preHash))).Replace("-", "");

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
        var categories = _myService.GetCategoriesForSingleId(id);

        return Ok(categories);
    }

}
