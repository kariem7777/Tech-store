using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TechCommerce.Models;
using TechCommerce.Repositories;

namespace TechCommerce.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IGenericRepository<Product> ProductRepository;

        public HomeController(ILogger<HomeController> logger, IGenericRepository<Product> ProductRepository)
        {
            _logger = logger;
            this.ProductRepository = ProductRepository;
        }

        public IActionResult Index()
        {
            List<Product> products = ProductRepository.GetAll();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
