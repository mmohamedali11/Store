﻿using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstraction;
using Services.Specifications;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork,IMapper mapper) : IProductService
    {
        

        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync(int? brandId, int? typeId, string? sort)
          {

             var spec = new ProductWithBrandsAndTypesSpecifications(brandId, typeId,sort );


            //Get all products Throught ProductRepository
            var products = await unitOfWork.GetRepository<Product,int>().GetAllAsync(spec   );

            //Mapping IEnumerable<Product> To IEnumerable<ProductResultDto> : AutoMapper
           var result = mapper.Map< IEnumerable<ProductResultDto>>(products);
            return result;
        }

        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandsAndTypesSpecifications(id);
            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(spec);
            if(product is null) return null; 
            var result = mapper.Map<ProductResultDto>(product);
            return result;
        }

        public async Task<IEnumerable<BrandResultDto>> GetAllBrandsAsync()
        {
            var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
          var result=  mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return result;
        }
 

        public async Task<IEnumerable<TypeResultDto>> GetAllTypesAsync()
        {
           var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<TypeResultDto>>(types);
            return result;
        }

        
    }
}
