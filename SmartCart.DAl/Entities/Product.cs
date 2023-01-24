using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCart.DAl.Entities
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; }

        public ProductCategory ProductCategory { get; set; } //navigational property
        public int ProductCategoryId { get; set; }

        public ProductBrand ProductBrand { get; set; }       //navigational property
        public int ProductBrandID { get; set; }

    }
}
