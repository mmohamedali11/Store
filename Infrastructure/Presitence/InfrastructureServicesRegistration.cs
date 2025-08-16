using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presistence.Identity;
using Presistence.Repositories;
using Presitence.Data;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence
{
    public static class InfrastructureServicesRegistration
    {
         public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(options =>
            {
                //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

            });

            services.AddDbContext<StoreIdentityDbContext>(options =>
            {
                //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseSqlServer(configuration["ConnectionStrings:IdentityConnection"]);

            });



            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();



            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!);
            });






            return services;
        }

    }
}
