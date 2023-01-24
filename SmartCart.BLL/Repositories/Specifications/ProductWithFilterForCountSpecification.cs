using SmartCart.BLL.Repositories.Specifications;
using SmartCart.DAl.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Talabat.BLL.Repositories.Specifications
{
    public class ProductWithFilterForCountSpecification : BaseSpecification<Product>
    {

        public ProductWithFilterForCountSpecification(ProductParams productParams)
        : base(p =>
                    (string.IsNullOrEmpty(productParams.Search) || (p.Name.ToLower().Contains(productParams.Search)))&&
                    (!productParams.CategoryId.HasValue || p.ProductCategoryId == productParams.CategoryId)&&
                    (!productParams.BrandId.HasValue || p.ProductBrandID == productParams.BrandId)
              )
        {

        }


    }
}
