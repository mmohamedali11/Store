    using Domain.Contracts;
using Domain.Models;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistence.Identity;
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
        private readonly StoreIdentityDbContext _identityDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(StoreDbContext context,
            StoreIdentityDbContext identityDbContext,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {

            _context = context;
            _identityDbContext = identityDbContext;
            _userManager = userManager;
            _roleManager = roleManager;

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

        public async Task InitializeIdentityAsync()
        {
            if (_identityDbContext.Database.GetPendingMigrations().Any())
            {
                await _identityDbContext.Database.MigrateAsync();
            }

            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "Admin"
                });
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = "SuperAdmin"
                });


            }


            if (!_userManager.Users.Any())
            {
                var SuperAdminUser = new AppUser()
                {
                    DisplayName = "SuperAdmin",
                    Email = "SuperAdmin@gmail.com",
                    UserName = "SuperAdmin",
                    PhoneNumber = "0123456789"
                };

                var AdminUser = new AppUser()
                {
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    UserName = "Admin",
                    PhoneNumber = "0123456789"
                };
                var superAdminResult = await _userManager.CreateAsync(SuperAdminUser, "P@ssW0rd123!");
                if (superAdminResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(SuperAdminUser, "SuperAdmin");
                }
                else
                {
                    Console.WriteLine($"SuperAdmin creation failed: {string.Join(", ", superAdminResult.Errors.Select(e => e.Description))}");
                }

                var adminResult = await _userManager.CreateAsync(AdminUser, "P@ssW0rd123!");
                if (adminResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(AdminUser, "Admin");
                }
                else
                {
                    Console.WriteLine($"Admin creation failed: {string.Join(", ", adminResult.Errors.Select(e => e.Description))}");
                }
            }
        }
         
    
        
    }
}
    

//..\Infrastructure\Presitence\Data\Seeding\types.json