using SmartCart.DAl.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.BLL.Repositories.Specifications;

namespace SmartCart.BLL.Repositories.Specifications
{
    public class ProductWithTypeAndBrandSpecification : BaseSpecification<Product> 
    {

        /// this constructor is used to get all products
        public ProductWithTypeAndBrandSpecification(ProductParams productParams)
            : base(p =>
             (string.IsNullOrEmpty(productParams.Search) || (p.Name.ToLower().Contains(productParams.Search)))&&
             (!productParams.CategoryId.HasValue || p.ProductCategoryId == productParams.CategoryId)&&
             (!productParams.BrandId.HasValue || p.ProductBrandID == productParams.BrandId)
            )
        {
            AddInclude(P=> P.ProductBrand);
            AddInclude(P=> P.ProductCategory);
            AddOrderBy(p => p.Name);

            ApplyPagination(productParams.PageSize *(productParams.PageIndex-1), productParams.PageSize);




            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }

        }


        // this constructor used to get a specific product
        public ProductWithTypeAndBrandSpecification(int id) : base(P => P.Id == id)
        {
            AddInclude(P => P.ProductBrand);
            AddInclude(P => P.ProductCategory);
        }
    }
}
