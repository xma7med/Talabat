﻿using AutoMapper;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using Microsoft.AspNetCore.Mvc;
using Talabat.Dashboard.Models;

namespace Talabat.Dashboard.Controllers
{
    public class BrandController (IUnitOfWork _unitOfWork, IMapper _mapper) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var mappedBrands = _mapper.Map<IEnumerable<ProductBrand>, IEnumerable<ProductBrandViewModel>>(Brands);
            return View(mappedBrands);
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductBrandViewModel model)
        {
            try
            {
                var mappedProduct = _mapper.Map<ProductBrandViewModel, ProductBrand>(model);
                await _unitOfWork.GetRepository<ProductBrand, int>().AddAsync(mappedProduct);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Name", "Please enter new name");
                return View(nameof(Index), await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync());
            }
        }
        public async Task<IActionResult> Delete(int Id)
        {
            var Brand = await _unitOfWork.GetRepository<ProductBrand, int>().GetAsync(Id);
            if (Brand == null)
                return NotFound();
            _unitOfWork.GetRepository<ProductBrand, int>().Delete(Brand);
            await _unitOfWork.CompleteAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
