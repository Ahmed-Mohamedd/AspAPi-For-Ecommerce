using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCart.DAl.Entities
{
    public class BasketItem:BaseEntity
    {
        public string ProductName { get; set; }
        public string Price { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
        public string Category { get; set; }
        public string Brand { get; set; }
    }
}
