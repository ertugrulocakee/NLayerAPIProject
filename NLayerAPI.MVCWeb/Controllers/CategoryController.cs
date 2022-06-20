using Microsoft.AspNetCore.Mvc;
using NLayerAPI.Core.Concrete;
using Newtonsoft.Json;
using NLayerAPI.Core.DTOs;
using NLayerAPI.MVCWeb.Services;

namespace NLayerAPI.MVCWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryAPIService _categoryApiService;

        public CategoryController(CategoryAPIService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoryApiService.GetAllAsync());  
        }


        public  IActionResult SaveCategory()
        {      
            return  View();
        }

        [HttpPost]
        public async Task<IActionResult> SaveCategory(CategoryDTO categoryDto)
        {

            if (ModelState.IsValid)
            {


                await _categoryApiService.SaveAsync(categoryDto);


                return RedirectToAction(nameof(Index));

            }


            return View();
        
        }

        [ServiceFilter(typeof(NotFoundFilter<Category>))]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var category = await _categoryApiService.GetByIdAsync(id);

            return View(category);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(CategoryDTO categoryDto)
        {
            if (ModelState.IsValid)
            {

                await _categoryApiService.UpdateAsync(categoryDto);

                return RedirectToAction(nameof(Index));

            }

         
            return View(categoryDto);

        }


        public async Task<IActionResult> Remove(int id)
        {
            await _categoryApiService.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
