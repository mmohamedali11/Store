﻿using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
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
        

        public async Task<PaginationResponse<ProductResultDto>> GetAllProductsAsync(ProductSpecificationsParamters specParams)
          {

             var spec = new ProductWithBrandsAndTypesSpecifications(specParams);  
            //Get all products Throught ProductRepository
            var products = await unitOfWork.GetRepository<Product,int>().GetAllAsync(spec);

            var result = mapper.Map< IEnumerable<ProductResultDto>>(products);

            var specCount = new ProductWithCountSpecifications(specParams);

            var count = await unitOfWork.GetRepository<Product, int>().CountAsync(specCount); 

            //var count = products.Count();

            //Mapping IEnumerable<Product> To IEnumerable<ProductResultDto> : AutoMapper


            return new PaginationResponse<ProductResultDto>(specParams.pageIndex,specParams.pageSize,count,result);
        }

        public async Task<ProductResultDto?> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithBrandsAndTypesSpecifications(id);
            var product = await unitOfWork.GetRepository<Product, int>().GetAsync(spec);
            if(product is null) throw new ProductNotFoundException(id);    
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
