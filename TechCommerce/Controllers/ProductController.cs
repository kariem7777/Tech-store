using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechCommerce.Models;
using TechCommerce.Repositories;
using TechCommerce.ViewModel;

namespace TechCommerce.Controllers
{
    public class ProductController : Controller
    {
        IproductRepository ProductRepository;
        IGenericRepository<Category> CategoryRepository;

        public ProductController(IproductRepository ProductRepo, IGenericRepository<Category> categoryRepository)  //Inject
        {
            ProductRepository = ProductRepo;
            CategoryRepository = categoryRepository;
        }

        // Read
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

            ProductsViewModel ProductsPagerViewModel = new ProductsViewModel()
            {
                products = filteredProducts,
                pagerViewModel = new PagerViewModel()
                {
                    Pager = pager,
                    SearchQuery = searchQuery
                },
                CategoryId = categoryId,
                Categories = CategoryRepository.GetAll()
            };

            return View("Index", ProductsPagerViewModel);
        }

        [Authorize]
        public IActionResult ShowDetails(int id)
        {
            Product? product = ProductRepository.GetWithRates(id);
            
            return product != null ? View("ShowDetails", product) : NotFound();
        }

        // Create
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            ProductViewModel productViewModel = new();

            productViewModel.Categories = CategoryRepository.GetAll();

            return View("Add", productViewModel);
        }

        [Authorize(Roles = "Admin")]

        public IActionResult SaveAdd(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                if (productViewModel.CategoryId != -1)
                {
                    Product product = new();

                    product.Name = productViewModel.Name;
                    product.Units = (int)productViewModel.Units;
                    product.Price = (int)productViewModel.Price;
                    product.Description = productViewModel.Description;
                    product.ImageUrl = productViewModel.ImageUrl;
                    product.CategoryId = productViewModel.CategoryId;

                    ProductRepository.Add(product);
                    ProductRepository.Save();

                    return RedirectToAction("Index");
                }
                else
                {
                    // Error Message & send to view
                    ModelState.AddModelError("CategoryId", "Category is required");
                }
            }

            productViewModel.Categories = CategoryRepository.GetAll();
            return View("Add", productViewModel);
        }

        // Update
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Product? product = ProductRepository.GetById(id);

            if (product != null)
            {
                ProductViewModel productViewModel = new ProductViewModel
                {
                    Id = id,
                    Name = product.Name,
                    Units = product.Units,
                    Price = product.Price,
                    Description = product.Description,
                    ImageUrl = product.ImageUrl,
                    CategoryId = product.CategoryId
                };

                productViewModel.Categories = CategoryRepository.GetAll();

                return View("Edit", productViewModel);
            }
            else
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult SaveEdit(ProductViewModel productViewModel, int id)
        {
            if (ModelState.IsValid)
            {
                Product productsDB = ProductRepository.GetById(id);

                if (productsDB != null)
                {
                    productsDB.Name = productViewModel.Name;
                    productsDB.Units = (int)productViewModel.Units;
                    productsDB.Price = (int)productViewModel.Price;
                    productsDB.Description = productViewModel.Description;
                    productsDB.ImageUrl = productViewModel.ImageUrl;
                    productsDB.CategoryId = productViewModel.CategoryId;

                    ProductRepository.Update(productsDB);
                    ProductRepository.Save();

                    return RedirectToAction("Index");
                }
            }
            productViewModel.Categories = CategoryRepository.GetAll();
            return View("Edit", productViewModel);
        }

        //Delete
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Product? product = ProductRepository.GetById(id);

            ProductRepository.Delete(id);
            ProductRepository.Save();

            return RedirectToAction("Index");
        }
    }
}
