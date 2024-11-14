using Microsoft.EntityFrameworkCore;
using Newslatter.Api.Context;

namespace Newslatter.Api.Extensions
{
    public static class ConfigureDatabase
    {
        public static void AddDatabaseConfigure(this IServiceCollection services, IConfigurationManager configuration)
        {
            services.AddDbContext<AppDbContext>(opt => 
                opt.UseSqlServer(configuration.GetConnectionString("SqlServer")));
        }
    }
}