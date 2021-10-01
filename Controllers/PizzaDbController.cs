using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ContosoPizza.Data;
using ContosoPizza.Models;

namespace ContosoPizza.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaDbController : ControllerBase
    {
        private readonly PizzaDbContext _context;
        public PizzaDbController(PizzaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Pizza>> GetAll() => _context.Pizzas.ToList();

        // GET by Id action
        [HttpGet("{id}")]
        public ActionResult<Pizza> Get(int id)
        {
            var pizza = _context.Pizzas.FirstOrDefault(x => x.Id == id);
            if(pizza == null) return NotFound();
            return pizza;
        }

        // POST action
        [HttpPost]
        public IActionResult Create(Pizza pizza)
        {
            _context.Pizzas.Add(pizza);
            _context.SaveChanges();
            return CreatedAtAction(nameof(Create), new { id = pizza.Id }, pizza);
        }
    }
}