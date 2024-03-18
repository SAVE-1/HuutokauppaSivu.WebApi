using Huutokauppa_sivu.Server.Models;
using Huutokauppa_sivu.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public IResult GetAll()
    {
        return Results.Ok(_myService.GetAll());
    }

    // GET all action
    [HttpGet("{id}")]
    public IResult GetAll(int id)
    {
        return Results.Ok(_myService.GetSingleFromDb(id));
    }

    // GET all action
    [HttpGet("PromotedItems")]
    public IResult GetPromoted()
    {
        return Results.Ok(_myService.GetPromotedItems());
    }

    // POST action
    [Authorize]
    [HttpGet("NewPosting")]
    public IResult Create(int price, string name, string description)
    {
        MD5 sum = MD5.Create();

        string preHash = name + DateTime.Now.ToString("HH:mm:ss tt") + "funky extra hash string thing";

        string deleteIdentification = BitConverter.ToString(sum.ComputeHash(Encoding.ASCII.GetBytes(preHash))).Replace("-", "");

        MagicalItem item = new MagicalItem()
        {
            Price = price,
            Name = name,
            Description = description,
            IsPromoted = false,
            PromotionImage = null,
            DeleteIdentification = deleteIdentification,
        };

        bool createNewResult = _myService.CreateNew(item);

        if (createNewResult)
        {
            return Results.Created("Created", item);
        }

        return Results.Problem();
    }

    [Authorize]
    [HttpDelete("{deleteIdentification}")]
    public IResult Delete(string deleteIdentification)
    {
        MagicalItem item = _myService.Delete(deleteIdentification);

        if (item != null)
        {
            return Results.Ok(item);
        }

        return Results.Problem();
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
