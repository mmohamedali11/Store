    using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Presitence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly StoreDbContext _context;
        public DbInitializer(StoreDbContext context)
        {

            _context= context;

            // Constructor logic if needed
        }

        public async Task InitializeAsync()
        {
            try
            {
                // Create the database if it does not exist && Apply To Any Pending Migrations
                if (_context.Database.GetPendingMigrations().Any())
                {
                    await _context.Database.MigrateAsync();
                }
                //Data Seeding
                //Seeding ProductTypes From Json File
                if (!_context.ProductTypes.Any())
                {

                    //1.Read All Data From Types Json File as string
                    var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Presitence\Data\Seeding\types.json  ");
                    //2.Transform the string to C# Object [List<ProductTypes>]
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    //3.Add the List<ProductTypes> to DataBase
                    if (types is not null && types.Any())
                    {
                        await _context.ProductTypes.AddRangeAsync(types);
                        await _context.SaveChangesAsync();
                    }
                }
                if (!_context.ProductBrands.Any())
                {

                    //1.Read All Data From Types Json File as string
                    var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Presitence\Data\Seeding\brands.json  ");
                    //2.Transform the string to C# Object [List<ProductBrand>]
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    //3.Add the List<ProductBrand> to DataBase
                    if (brands is not null && brands.Any())
                    {
                        await _context.ProductBrands.AddRangeAsync(brands);
                        await _context.SaveChangesAsync();
                    }
                }
                if (!_context.Products.Any())
                {
                    var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Presitence\Data\Seeding\products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    if (products is not null && products.Any())
                    {
                        await _context.Products.AddRangeAsync(products);
                        await _context.SaveChangesAsync();
                    }
                }


                //Seeding ProductTypes From Json File
                //1.Read All Data From Types Json File as string
                //2.Transform the string to C# Object [List<ProductTypes>]
                //3.Add the List<ProductTypes> to DataBase



                //Seeding ProductBrands From Json File
                //Seeding Product From Json File
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
//..\Infrastructure\Presitence\Data\Seeding\types.json