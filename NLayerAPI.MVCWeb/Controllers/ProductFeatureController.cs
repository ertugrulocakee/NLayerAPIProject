using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayerAPI.Core.Concrete;
using NLayerAPI.Core.DTOs;
using NLayerAPI.MVCWeb.Services;

namespace NLayerAPI.MVCWeb.Controllers
{
    public class ProductFeatureController : Controller
    {

        private readonly ProductAPIService _productApiService;
        private readonly ProductFeatureAPIService _productFeatureApiService;

        public ProductFeatureController(ProductAPIService productApiService, ProductFeatureAPIService productFeatureApiService)
        {
            _productApiService = productApiService;
            _productFeatureApiService = productFeatureApiService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _productFeatureApiService.GetProductFeaturesWithProductAsync());
        }

        public async Task<IActionResult> SaveProductFeature()
        {
            var productsDto = await _productApiService.GetProductsWithCategoryAsync();

            ViewBag.products = new SelectList(productsDto, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveProductFeature(ProductFeatureDTO productFeatureDto)
        {


            if (ModelState.IsValid)
            {

                await _productFeatureApiService.SaveAsync(productFeatureDto);


                return RedirectToAction(nameof(Index));
            }

            var productsDto = await _productApiService.GetProductsWithCategoryAsync();

            ViewBag.products = new SelectList(productsDto, "Id", "Name");

            return View();

        }

  
        public async Task<IActionResult> UpdateProductFeature(int id)
        {
            var productFeature = await _productFeatureApiService.GetByIdAsync(id);

            var productsDto = await _productApiService.GetProductsWithCategoryAsync();

            ViewBag.products = new SelectList(productsDto, "Id", "Name");

            return View(productFeature);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateProductFeature(ProductFeatureDTO productFeatureDto)
        {
            if (ModelState.IsValid)
            {

                await _productFeatureApiService.UpdateAsync(productFeatureDto);

                return RedirectToAction(nameof(Index));

            }

            var productsDto = await _productApiService.GetProductsWithCategoryAsync();

            ViewBag.products = new SelectList(productsDto, "Id", "Name");

            return View(productFeatureDto);
        }


        public async Task<IActionResult> Remove(int id)
        {
            await _productFeatureApiService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
