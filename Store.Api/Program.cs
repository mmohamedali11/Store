 
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Presitence.Data;
using Services;
using Services.Abstraction;
using Shared.ErrorsModels;
using Store.Api.Extensions;
using Store.Api.Middlewares;
using System.Reflection.Metadata;
using System.Threading.Tasks;



namespace Store.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.RigsterAllServices(builder.Configuration);


            var app = builder.Build();
            // Configure the HTTP request pipeline.

            await app.ConfigureMiddlewares();



            app.Run();
        }
    }
}
