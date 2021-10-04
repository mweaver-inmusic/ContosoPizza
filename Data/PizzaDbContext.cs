using Microsoft.EntityFrameworkCore;
using ContosoPizza.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ContosoPizza.Data
{
    public class PizzaDbContext : IdentityDbContext
    {
        public PizzaDbContext(DbContextOptions<PizzaDbContext> options) : base(options)
        {            
        }

        public DbSet<Pizza> Pizzas { get; set; }
    }
}