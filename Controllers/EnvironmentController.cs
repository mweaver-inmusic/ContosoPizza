using System;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EnvironmentController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return Environment.GetEnvironmentVariable("DATABASE_URL");
        }
    }
}