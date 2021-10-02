using System;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EnvironmentController : ControllerBase
    {
        [HttpGet]
        public DbUrl Get()
        {
            var dburl = Environment.GetEnvironmentVariable("DATABASE_URL");
            return new DbUrl { Str = dburl };
        }
    }

    public class DbUrl
    {
        public string Str { get; set; }
    }
}