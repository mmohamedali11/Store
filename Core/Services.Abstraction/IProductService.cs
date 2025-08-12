﻿using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IProductService
    {
        //Get all products
        Task<IEnumerable<ProductResultDto>> GetAllProductsAsync();
        //Get product by id
        Task<ProductResultDto?>GetProductByIdAsync(int id);
        //Get All Brands 
        Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync();
        //Get All Types
        Task<IEnumerable<TypeResultDto>> GetAllTypesAsync();

    }
}
