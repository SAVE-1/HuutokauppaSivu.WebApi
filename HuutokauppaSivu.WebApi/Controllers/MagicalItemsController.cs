using Huutokauppa_sivu.Server.Models;
using Huutokauppa_sivu.Server.Services;
using Microsoft.AspNetCore.Mvc;

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
    public ActionResult<List<MagicalItem>> GetAll() {
        return _myService.GetAll();
    }

    // GET all action
    [HttpGet("{id}")]
    public ActionResult<MagicalItem> GetAll(int id)
    {
        return _myService.GetSingleFromDb(id);
    }

    // GET all action
    [HttpGet("PromotedItems")]
    public ActionResult<List<MagicalItem>> GetPromoted()
    {
        return _myService.GetPromotedItems();
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
