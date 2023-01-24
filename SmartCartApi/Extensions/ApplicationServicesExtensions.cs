using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SmartCart.Api.Errors;
using SmartCart.Api.Helpers;
using SmartCart.BLL.Interfaces;
using SmartCart.BLL.Repositories;
using SmartCart.BLL.Services;
using System.Linq;




namespace Talabat.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            services.AddScoped(typeof(ITokenService), typeof(TokenService));
            services.AddScoped(typeof(IOrderService), typeof(OrderService));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            //services.AddScoped<IGenericRepository<>, GenericRepository<>>(); // cause i dont know the kind of T here  
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddAutoMapper(typeof(MappingProfile));
            //services.AddAutoMapper(typeof(AppUserMappingProfile));


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState.Where(m => m.Value.Errors.Count > 0)
                                                         .SelectMany(m => m.Value.Errors)
                                                         .Select(e => e.ErrorMessage).ToArray();
                    var ResponseMessage = new ValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(ResponseMessage);
                };

            });

           

            return services;
        }
    }
}
