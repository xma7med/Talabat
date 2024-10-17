﻿using LinkDev.Talabat.Core.Application.Abstraction.Models.Basket;

namespace LinkDev.Talabat.Core.Application.Abstraction.Services.Basket
{
	public interface IBasketService
	{
		Task<CustomerBasketDto> GetCustomerBasketAsync(string basketId);

		Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto BasketDto);

		Task DeleteCustomerBasketAsync(string basketId);

	}
}
