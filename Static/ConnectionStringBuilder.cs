using System;
using Npgsql;

namespace ContosoPizza.Static
{
    public static class ConnectionStringBuilder
    {
        public static string GetHerokuPostgresqlDbConnectionString()
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
            return builder.ToString();
        }
    }
}