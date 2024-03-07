using Microsoft.AspNetCore.Mvc;
using Huutokauppa_sivu.Server.Models;
using Huutokauppa_sivu.Server.Services;

namespace Huutokauppa_sivu.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class MagicalItemsController
{
    public MagicalItemsController()
    {

    }

    // GET all action
    [HttpGet(Name = "GetMagicalItems")]
    public ActionResult<List<MagicalItem>> GetAll() {
        return MagicalItemsService.GetAll();
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
