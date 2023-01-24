using SmartCart.BLL.Interfaces;
using SmartCart.DAl.Entities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SmartCart.BLL.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteCustomerBasket(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetCustomerBasket(string basketId) // as Get or ReCreate 
        {
            var basket = await _database.StringGetAsync(basketId);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> UpdateCustomerBasket(CustomerBasket customerBasket) // as Update or Create for 1st time.
        {
            var created = await _database.StringSetAsync(customerBasket.ID ,JsonSerializer.Serialize(customerBasket) , TimeSpan.FromDays(30));
            if (!created) return null;
            return await GetCustomerBasket(customerBasket.ID);
        }
    }
}
