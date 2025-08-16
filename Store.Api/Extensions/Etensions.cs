using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presistence;
using Presistence.Identity;
using Services;
using Shared.ErrorsModels;
using Store.Api.Middlewares;

namespace Store.Api.Extensions
{
    public static class Etensions
    {
        public static IServiceCollection RigsterAllServices(this IServiceCollection services , IConfiguration configuration)
        {
            // Add services to the container.

          services.AddBuiltInServices(); //Add Built-in Services
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
           services.AddSwaggerServices(); //Add Swagger Services
            services.ConfigureServices(); //Configure Services


            services.AddInfrastructureServices(configuration); //Add Infrastructure Services
            services.AddIdentityServices(); //Add Identity Services
            services.AddApplicationServices();

           


            return services;
        }

        private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
        {
            services.AddControllers();

            return services;
        }


        private static IServiceCollection AddIdentityServices(this IServiceCollection services)
        {
            services.AddIdentity<AppUser,IdentityRole>()
                .AddEntityFrameworkStores<StoreIdentityDbContext>();

            return services;
        }

        private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }


        private static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actioncontext) =>
                {
                    var errors = actioncontext.ModelState.Where(m => m.Value.Errors.Any())
                                             .Select(m => new ValidationError()
                                             {
                                                 Field = m.Key,
                                                 Errors = m.Value.Errors.Select(errors => errors.ErrorMessage)

                                             });

                    var response = new ValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });
            return services;
        }



        public static async Task<WebApplication> ConfigureMiddlewares(this WebApplication app) 
        {

          await app.InitializeDatabaseAsync();
           
            app.UseGlobalErrorHandling();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            return app;

        }

        private static async Task<WebApplication> InitializeDatabaseAsync(this WebApplication app)
        {
            #region Seeding

            using var scope = app.Services.CreateScope();
            var dbInitialzer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();//Ask Clr Create object From DbInitializer
            await dbInitialzer.InitializeIdentityAsync(); //Call InitializeIdentityAsync Method to Create Identity Database and Seed Data
            await dbInitialzer.InitializeAsync(); //Call InitializeAsync Method to Create Database and Seed Data 
            #endregion


            return app;

        }


        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddlware>();

            return app;

        }



    }
}
