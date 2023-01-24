using System.Collections.Generic;

namespace SmartCart.Api.Dtos
{
    public class CustomerBasketDto
    {
        public string ID { get; set; }
        public List<BasketItemDto> Items { get; set; } = new List<BasketItemDto>();

    }
}
