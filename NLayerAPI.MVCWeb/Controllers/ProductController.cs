using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NLayerAPI.Core.Concrete;
using NLayerAPI.Core.DTOs;
using NLayerAPI.Core.Services;
using NLayerAPI.MVCWeb.Services;

namespace NLayerAPI.MVCWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductAPIService _productApiService;
        private readonly CategoryAPIService _categoryApiService;

        public ProductController(ProductAPIService productApiService, CategoryAPIService categoryApiService)
        {
            _productApiService = productApiService;
            _categoryApiService = categoryApiService;   
        }

        public async Task<IActionResult> Index()
        {

            return View(await _productApiService.GetProductsWithCategoryAsync());
        }


        public async Task<IActionResult> Save()
        {
            var categoriesDto = await _categoryApiService.GetAllAsync();

            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDTO productDto)

        {


            if (ModelState.IsValid)
            {

                await _productApiService.SaveAsync(productDto);


                return RedirectToAction(nameof(Index));
            }

            var categoriesDto = await _categoryApiService.GetAllAsync();



            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");
            return View();
        }

        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productApiService.GetByIdAsync(id);


            var categoriesDto = await _categoryApiService.GetAllAsync();



            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", product.CategoryId);

            return View(product);

        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDTO productDto)
        {
            if (ModelState.IsValid)
            {

                await _productApiService.UpdateAsync(productDto);

                return RedirectToAction(nameof(Index));

            }

            var categoriesDto = await _categoryApiService.GetAllAsync();



            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", productDto.CategoryId);

            return View(productDto);

        }


        public async Task<IActionResult> Remove(int id)
        {
            await _productApiService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
