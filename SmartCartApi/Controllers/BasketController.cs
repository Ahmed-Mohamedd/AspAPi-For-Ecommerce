using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCart.Api.Dtos;
using SmartCart.BLL.Interfaces;
using SmartCart.DAl.Entities;
using System.Threading.Tasks;

namespace SmartCart.Api.Controllers
{
    
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository , IMapper mapper)
        {
            _basketRepository=basketRepository;
            _mapper=mapper;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string basketId)
        {
            var basket  = await _basketRepository.GetCustomerBasket(basketId);
            return Ok(basket ?? new CustomerBasket(basketId));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basketDto)
        {
            var mappedBasket =  _mapper.Map<CustomerBasketDto, CustomerBasket>(basketDto);
            var CustomerBasket = await _basketRepository.UpdateCustomerBasket(mappedBasket);
            return Ok(CustomerBasket);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string basketId)
        {
            return await _basketRepository.DeleteCustomerBasket(basketId);
        }


    }
}
