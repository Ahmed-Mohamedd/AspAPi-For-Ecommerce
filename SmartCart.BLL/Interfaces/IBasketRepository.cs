using SmartCart.DAl.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCart.BLL.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetCustomerBasket(string basketId);
        Task<CustomerBasket> UpdateCustomerBasket(CustomerBasket customerBasket);
        Task<bool> DeleteCustomerBasket(string basketId);
    }
}
