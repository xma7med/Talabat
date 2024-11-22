using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Exception;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Core.Application.Services.Basket
{
	internal class BasketService(IBasketRepository basketRepository , IMapper mapper , IConfiguration configuration) : IBasketService
	{
		public async Task<CustomerBasketDto> GetCustomerBasketAsync(string basketId)
		{
			var basket = await basketRepository.GetAsync(basketId);
			if (basket is null) throw new NotFoundException( nameof(CustomerBasket) , basketId);
			return mapper.Map<CustomerBasketDto>(basket);	
		}
		public async Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto BasketDto)
		{
			var basket = mapper.Map<CustomerBasket>(BasketDto);
			var timeToLive = TimeSpan.FromDays(double.Parse(configuration.GetSection("RedisSettings")["TimeToLiveInDays"]!));
			var updatedBasket = await basketRepository.UpdateAsync(basket , timeToLive);
			if (updatedBasket is null) throw new BadRequestException("Can't Update , There are  a problem with your basket ");
			return BasketDto;


		}
		public async Task DeleteCustomerBasketAsync(string basketId)
		{
			 var deleted = await basketRepository.DeleteAsync(basketId);
			if (!deleted) throw new BadRequestException("unable to delete this basket ");	
		}

		
	}
}
