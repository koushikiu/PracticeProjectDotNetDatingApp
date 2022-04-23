using DataModel.Data;
using DataModel.Repositories;
using services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceCollection
    {
        public static IServiceCollection AddApplictionServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IUserManagementService, UserManagementService>();
            services.AddTransient<ITockenService, TockenService>();

            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            return services;    
        }
    }
}
