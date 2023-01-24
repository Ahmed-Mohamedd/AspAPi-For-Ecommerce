using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCart.Api.Dtos;
using SmartCart.Api.Errors;
using SmartCart.BLL.Interfaces;
using SmartCart.DAl.Entities.Order_Aggregate;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartCart.Api.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService , IMapper mapper)
        {
            _orderService=orderService;
            _mapper=mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var orderAddress = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);
            var order  = await _orderService.CreateOrderAsync(buyerEmail , orderDto.BasketId ,orderDto.DeliveryMethod , orderAddress);
            
            if (order == null) return BadRequest(new ApiResponse(400, "An Error Occured During Creating the order"));

            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser()
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var orders = await  _orderService.GetOrdersForUserAsync(buyerEmail);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderForUser(int id)
        {
            var buyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrderByIdForUser(id,buyerEmail);
            return Ok(order);
        }

        [HttpGet("deliverMethods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliverMethods(int id)
        {
            return Ok(await _orderService.GetDeliveryMethodAsync());
        }
    }
}
