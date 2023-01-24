namespace SmartCart.Api.Dtos
{
    public class ProductToReturnDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public string PictureUrl { get; set; }

        public string ProductBrand { get; set; } // navigational property

        public int ProductBrandId { get; set; }

        public string ProductCategory { get; set; } // navigational property

        public int ProductCategoryId { get; set; }
    }
}
