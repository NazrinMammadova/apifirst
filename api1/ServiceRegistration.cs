using api1.Data;
using Microsoft.EntityFrameworkCore;

namespace api1
{
    public static class ServiceRegistration
    {
        public static void Register(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
        }
    }
}
