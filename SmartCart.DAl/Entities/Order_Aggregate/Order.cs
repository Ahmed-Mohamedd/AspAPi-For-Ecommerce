using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCart.DAl.Entities.Order_Aggregate
{
    public class Order:BaseEntity
    {
        public Order()
        {
                
        }
        public Order(string buyerEmail, List<OrderItem> items, Address shipToAddress, DeliveryMethod deliveryMethod, decimal subtotal)
        {
            BuyerEmail=buyerEmail;
            Items=items;
            ShipToAddress=shipToAddress;
            DeliveryMethod=deliveryMethod;
            Subtotal=subtotal;
        }

        public string BuyerEmail { get; set; }
        public List<OrderItem> Items { get; set; }
        public Address ShipToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public int PaymentIntentId { get; set; }

        public decimal Subtotal { get; set; }

        public decimal GetTotal()
            => Subtotal + DeliveryMethod.Cost;
    }
}
