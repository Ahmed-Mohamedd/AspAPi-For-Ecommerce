using SmartCart.DAl.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCart.BLL.Repositories.Specifications.Order_Specifications
{
    public class OrdersWithItemsAndDeliveryMethodsSpecification:BaseSpecification<Order>
    {

        // this constructor is used to get All orders for a specific User
        public OrdersWithItemsAndDeliveryMethodsSpecification(string buyerEmail):base(o => o.BuyerEmail == buyerEmail)
        {
            AddInclude(o => o.Items);
            AddInclude(o => o.DeliveryMethod);
            AddOrderByDescending(o => o.OrderDate);

        }

        // this constructor is used to get An order for a specific User
        public OrdersWithItemsAndDeliveryMethodsSpecification( int orderId, string buyerEmail) 
            :base(o => (o.BuyerEmail == buyerEmail && o.Id == orderId))
        {
            AddInclude(o => o.Items);
            AddInclude(o => o.DeliveryMethod);


        }
    }
}
