using AutoMapper;
using LinkDev.Talabat.Core.Application.Abstraction.Models.Product;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Core.Domain.Specifications.Product_Specs;
using Microsoft.AspNetCore.Mvc;
using Talabat.Dashboard.Helpers;
using Talabat.Dashboard.Models;

namespace Talabat.Dashboard.Controllers
{
    public class ProductController(IUnitOfWork _unitOfWork, IMapper _mappper) : Controller
    {
        public async Task<IActionResult> Index(ProductSpecParams specParams)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(specParams.Sort, specParams.BrandId, specParams.CategoryId, specParams.PageSize, specParams.PageIndex, specParams.Search);

            var products = await _unitOfWork.GetRepository<Product, int>().GetAllWithSpecAsync(spec);

            var total = await _unitOfWork.GetRepository<Product, int>().GetCountAsync(new ProductWithBrandAndCategorySpecifications(
                specParams.Sort, specParams.BrandId, specParams.CategoryId, int.MaxValue, 1, specParams.Search
            ));

            var mappedProducts = _mappper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);

            var viewModel = new PaginatedProductViewModel
            {
                Products = mappedProducts,
                PageIndex = specParams.PageIndex,
                PageSize = specParams.PageSize,
                TotalCount = total
            };

            return View(viewModel);
        }



        //-------------------------------------------------------------------------------------------------


        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    var Products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync();
        //    var mappedProducts = _mappper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(Products);
        //    return View(mappedProducts);
        //}

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                if (productViewModel.Image != null)
                {
                    productViewModel.PictureUrl = PictureSettings.UploadFile(productViewModel.Image, "products");
                }
                else 
                {
                    productViewModel.PictureUrl = "images/products/glazed-donuts.png";
                }
                var mappedProduct = _mappper.Map<ProductViewModel, Product>(productViewModel);
                await _unitOfWork.GetRepository<Product, int>().AddAsync(mappedProduct);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError(string.Empty, "An Errror Happend While Creating The Product");
            return View(productViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var Product = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);
            if (Product is null) return NotFound();
            var mappedProduct = _mappper.Map<Product, ProductViewModel>(Product);
            return View(mappedProduct);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductViewModel model)
        {
            if (id != model.Id)
                return NotFound();
            if (ModelState.IsValid)
            {
                if (model.Image != null)
                {
                    if (model.PictureUrl != null)
                    {
                        PictureSettings.DeleteFile(model.PictureUrl, "products");
                    }
                    model.PictureUrl = PictureSettings.UploadFile(model.Image, "products");
                }
                var mappedProduct = _mappper.Map<ProductViewModel, Product>(model);
                _unitOfWork.GetRepository<Product, int>().Update(mappedProduct);
                var Result = await _unitOfWork.CompleteAsync();
                if (Result > 0)
                    RedirectToAction(nameof(Index));
            }
            return View(model);
        }



        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var Product = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);
            if (Product == null)
                return NotFound();
            var mappedProduct = _mappper.Map<Product, ProductViewModel>(Product);
            return View(mappedProduct);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id, ProductViewModel model)
        {
            if (id != model.Id) 
                return NotFound();
            try
            {
                var Product = await _unitOfWork.GetRepository<Product, int>().GetAsync(id);
                if (Product.PictureUrl != null)
                {
                    PictureSettings.DeleteFile(Product.PictureUrl, "products");
                }
                _unitOfWork.GetRepository<Product, int>().Delete(Product);
                await _unitOfWork.CompleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }


    }
}
