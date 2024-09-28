using Microsoft.AspNetCore.Mvc;

namespace TechCommerce.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
