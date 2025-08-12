 
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Presitence.Data;
using Services;
using Services.Abstraction;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using AssemblyMapping = Services.AssemblyReference; 


namespace Store.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreDbContext>(options =>
            {
                //options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);

            });
            builder.Services.AddScoped<IDbInitializer, DbInitializer>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
           builder.Services.AddScoped<IServiceManager, ServiceManager>();
            builder.Services.AddAutoMapper(typeof(AssemblyMapping ).Assembly);

            var app = builder.Build();
            #region Seeding

            using var scope = app.Services.CreateScope();
            var dbInitialzer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();//Ask Clr Create object From DbInitializer
            await dbInitialzer.InitializeAsync(); //Call InitializeAsync Method to Create Database and Seed Data 
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
