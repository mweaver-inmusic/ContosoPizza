using System.Data;
using System;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

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
            var uri = new Uri(dburl);
            var userInfo = uri.UserInfo.Split(":");
            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = uri.Host,
                Port = uri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = uri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Prefer,
                TrustServerCertificate = true
            };
            return new DbUrl { 
                FullString = dburl,
                Host = uri.Host,
                Port = uri.Port.ToString(),
                Username = userInfo[0],
                Password = userInfo[1],
                Database = uri.LocalPath.TrimStart('/'),
                ConnectionString = builder.ToString()
            };
        }
    }

    public class DbUrl
    {
        public string FullString { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string ConnectionString { get; set; }
    }
}