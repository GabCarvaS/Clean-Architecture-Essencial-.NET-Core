using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategories();
            return View(categories);
        }

        [HttpGet()]
        public IActionResult Create()
        {           
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return NotFound(); }

            var category = await _categoryService.GetById(id);

            if (category == null) { return NotFound(); } 
            
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }

            var category = await _categoryService.GetById(id);

            if (category == null) { return NotFound(); }

            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) { return NotFound(); }

            var category = await _categoryService.GetById(id);

            if (category == null) { return NotFound(); }

            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDto)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.Add(categoryDto);
                return RedirectToAction(nameof(Index));
            }            
            return View(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryDTO categoryDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.Update(categoryDto);
                }
                catch (Exception) 
                {
                    throw;
                }
                
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDto);
        }

        [HttpPost(), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            await _categoryService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
