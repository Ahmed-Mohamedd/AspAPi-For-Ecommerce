using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartCart.Api.Dtos;
using SmartCart.Api.Errors;
using SmartCart.BLL.Interfaces;
using SmartCart.BLL.Repositories.Specifications;
using SmartCart.DAl.Entities;
using SmartCartApi.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.BLL.Repositories.Specifications;

namespace SmartCart.Api.Controllers
{
    [Authorize]
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brandRepo;
        private readonly IGenericRepository<ProductCategory> _categRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> ProductRepo , IGenericRepository<ProductBrand> BrandRepo, IGenericRepository<ProductCategory> categRepo, IMapper mapper)
        {
            _productRepo=ProductRepo;
            _brandRepo=BrandRepo;
            _categRepo=categRepo;
            _mapper=mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery] ProductParams productParams)
        {
            //var Products = await  _productRepo.GetAllAsync();
            var spec = new ProductWithTypeAndBrandSpecification(productParams);
            var products = await _productRepo.GetAllWithSpecAsync(spec);
            var Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            var countSpec = new ProductWithFilterForCountSpecification(productParams);
            var count = await _productRepo.GetCountAsync(countSpec);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize,count, Data));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            
            var spec = new ProductWithTypeAndBrandSpecification(id);
            var product = await _productRepo.GetByIdWithSpecAsync(spec);
            if (product == null) return NotFound(new ApiResponse(404));

            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _brandRepo.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetTypes()
        {
            var Types = await _categRepo.GetAllAsync();
            return Ok(Types);
        }

    }
}
