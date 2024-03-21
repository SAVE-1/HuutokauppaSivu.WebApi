using Huutokauppa_sivu.Server.Models;
using Huutokauppa_sivu.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Huutokauppa_sivu.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class MagicalItemsController : ControllerBase
{
    private readonly IItem _myService;

    public MagicalItemsController(IItem myService)
    {
        _myService = myService;
    }

    // GET all action
    [HttpGet("")]
    public IActionResult GetAll(int skip = 0, int take = 50)
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
    public IActionResult GetSingleById(int id)
    {
        return Ok(_myService.GetSingleFromDb(id));
    }

    // GET all action
    [HttpGet("PromotedItems")]
    public IActionResult GetPromoted(int skip = 0, int take = 50)
    {
        return Ok(_myService.GetPromotedItems(skip, take));
    }

    // POST action
    [Authorize]
    [HttpGet("NewPosting")]
    public IActionResult Create(int price, string name, string description)
    {
        MD5 sum = MD5.Create();

        string preHash = name + DateTime.Now.ToString("HH:mm:ss tt") + "funky extra hash string thing";

        // create a secondary ID, in order not to reveal the internal structure or additional info of the database to the browser
        string deleteIdentification = BitConverter.ToString(sum.ComputeHash(Encoding.ASCII.GetBytes(preHash))).Replace("-", "");

        MagicalItem item = new MagicalItem()
        {
            Price = price,
            Name = name,
            Description = description,
            IsPromoted = false,
            PromotionImage = null,
            DeleteIdentification = deleteIdentification,
            CreatedBy = User.FindFirstValue(ClaimTypes.Name)
        };

        bool createNewResult = _myService.CreateNew(item);

        if (createNewResult)
        {
            return Created("Created", item);
        }

        return Problem();
    }

    [Authorize]
    [HttpDelete("{deleteIdentification}")]
    public IActionResult Delete(string deleteIdentification)
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


    // GET by Id action
    //[HttpGet("{id}")]
    //public ActionResult<MagicalItem> Get(int id)
    //{
    //    //var pizza = PizzaService.Get(id);

    //    //if (pizza == null)
    //    //    return NotFound();

    //    return pizza;
    //}

    // POST action
    // [HttpPost]
    // public IActionResult Create(Pizza pizza)
    // {
    //     // This code will save the pizza and return a result
    // }

    // // PUT action
    // [HttpPut("{id}")]
    // public IActionResult Update(int id, Pizza pizza)
    // {
    //     // This code will update the pizza and return a result
    // }

    // // DELETE action
    // [HttpDelete("{id}")]
    // public IActionResult Delete(int id)
    // {
    //     // This code will delete the pizza and return a result
    // }


}
