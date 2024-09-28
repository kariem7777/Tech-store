using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechCommerce.Models;
using TechCommerce.Repositories;

namespace TechCommerce.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository ProductRepository;

        public ProductController(IProductRepository ProductRepo)  //Inject
        {
            ProductRepository = ProductRepo; //new ProductRepository();
        }

        public IActionResult Index()
        {
            List<Product> productsList = ProductRepository.GetAll();
            return View("Index", productsList);
        }
    }
}
