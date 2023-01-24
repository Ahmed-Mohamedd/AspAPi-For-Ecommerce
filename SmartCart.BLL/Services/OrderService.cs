using SmartCart.BLL.Interfaces;
using SmartCart.BLL.Repositories.Specifications.Order_Specifications;
using SmartCart.DAl.Entities;
using SmartCart.DAl.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCart.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepo;
        private readonly IGenericRepository<Order> _ordersRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepository ,/* IGenericRepository<Product> productsRepo,*/
            //IGenericRepository<DeliveryMethod> deliveryMethodRepo , IGenericRepository<Order> ordersRepo,
             IUnitOfWork unitOfWork)
        {
            _basketRepository=basketRepository;
            //_productsRepo=productsRepo;
            //_deliveryMethodRepo=deliveryMethodRepo;
            //_ordersRepo=ordersRepo;
            _unitOfWork=unitOfWork;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodID, Address shipToAddress)
        {
            //1- get basket from basket repo
            var basket = await _basketRepository.GetCustomerBasket(basketId);

            //2- get selected items at basket from product repo 
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);
                var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);
                
                orderItems.Add(orderItem);
            }

            //3- get delivery method from delivery method repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodID);

            //4- calc subtotal
            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);

            //5- create order
            var order = new Order(buyerEmail, orderItems, shipToAddress, deliveryMethod, subtotal);
            await _unitOfWork.Repository<Order>().Add(order);

            //6- save to database
            int res = await _unitOfWork.Complete();
            if(res<=0) return null;


            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndDeliveryMethodsSpecification(buyerEmail);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return orders;
        }
        public async Task<Order> GetOrderByIdForUser(int orderId, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndDeliveryMethodsSpecification(orderId,buyerEmail);
            var order = await _unitOfWork.Repository<Order>().GetByIdWithSpecAsync(spec);
            return order;
        }
        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
        }


    }
}
