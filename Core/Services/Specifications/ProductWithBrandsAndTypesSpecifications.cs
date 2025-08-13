using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ProductWithBrandsAndTypesSpecifications : BaseSpecifications<Product, int>
    {
        public  ProductWithBrandsAndTypesSpecifications(int id) : base(p => p.Id == id)
        {

            ApplyIncludes();
        }

        public ProductWithBrandsAndTypesSpecifications(int? brandId, int? typeId, string? sort)
            : base

            (

                p =>
                    (!brandId.HasValue || p.BrandId == brandId) &&
                         (!typeId.HasValue || p.TypeId == typeId)
            )


        {
            ApplyIncludes();
            ApplySorting(sort);


        }
        private void ApplyIncludes()
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }



        private void ApplySorting(string? sort) 
        {
            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToLower())
                {
                    case "nameasc":
                        AddOrderBy(p => p.Name);
                        break;
                    case "namedesc":
                        AddOrderByDescending(p => p.Name);
                        break;
                    case "priceasc":
                        AddOrderBy(p => p.price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(p => p.price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;



                }
            }
            else
            {
                AddOrderBy(p => p.Name);

            }

        }

    }
}
