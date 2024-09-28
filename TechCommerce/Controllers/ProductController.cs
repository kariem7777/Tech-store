using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechCommerce.Models;
using TechCommerce.Repositories;

namespace TechCommerce.Controllers
{
    public class ProductController : Controller
    {
        IRepository<Product> ProductRepository;
        IRepository<Category> CategoryRepository;

        public ProductController(IRepository<Product> ProductRepo, IRepository<Category> categoryRepository)  //Inject
        {
            ProductRepository = ProductRepo; 
            CategoryRepository = categoryRepository;
        }

        public IActionResult Index(string searchQuery = "", int categoryId = 0, int pg = 1)
        {
            List<Product> products = ProductRepository.GetAll();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                products = products.Where(s => s.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (categoryId > 0)
            {
                products = products.Where(p => p.CategoryId == categoryId).ToList();
            }

            const int pageSize = 8;
            if (pg < 1)
                pg = 1;

            var pager = new Pager(products.Count, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            List<Product> filteredProducts = products.Skip(recSkip).Take(pager.PageSize).ToList();

            ProductsSearchPagerViewModel studentsPagerViewModel = new ProductsSearchPagerViewModel()
            {
                products = filteredProducts,
                Pager = pager,
                SearchQuery = searchQuery,
                CategoryId = categoryId,
                Categories = CategoryRepository.GetAll()
            };

            return View("Index", studentsPagerViewModel); 
        }

        [Authorize]
        public IActionResult ShowDetails(int id)
        {
            Product? product = ProductRepository.GetById(id);

            return product != null ? View("ShowDetails", product) : NotFound();
        }
    }
}
