using ApiTemplate.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace ApiTemplate.Config
{
    public static class SqlConnection
    {
        public static IServiceCollection ConfigDbConnection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration["DATABASE_CONNECTION_STRING"]));
            return services;
        }
    }
}
