using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Basket;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Domain.Contracts.Infrastructure;
using LinkDev.Talabat.Core.Domain.Entities.Basket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services.Basket
{
	internal class BasketService(IBasketRepository basketRepository , IMapper mapper , IConfiguration configuration) : IBasketService
	{
		public async Task<CustomerBasketDto> GetCustomerBasketAsync(string basketId)
		{
			var basket = await basketRepository.GetAsync(basketId);
			if (basket is null) throw new Exception();
			return mapper.Map<CustomerBasketDto>(basket);	
		}
		public async Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto BasketDto)
		{
			var basket = mapper.Map<CustomerBasket>(BasketDto);
			var timeToLive = TimeSpan.FromDays(double.Parse(configuration.GetSection("RedisSettings")["TimeToLiveInDays"]!));
			var updatedBasket = await basketRepository.UpdateAsync(basket , timeToLive);
			if (updatedBasket is null) throw new Exception();
			return BasketDto;


		}
		public async Task DeleteCusatomerBasketAsync(string basketId)
		{
			 var deleted = await basketRepository.DeleteAsync(basketId);
			if (!deleted) throw new Exception();	
		}


	}
}
