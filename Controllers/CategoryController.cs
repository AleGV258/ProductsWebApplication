using IGRTEC_WEBApplication_MichellAlejandroGarciaVargas.Models;
using IGRTEC_WEBApplication_MichellAlejandroGarciaVargas.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IGRTEC_WEBApplication_MichellAlejandroGarciaVargas.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            return View(categories);
        }
    }
}
