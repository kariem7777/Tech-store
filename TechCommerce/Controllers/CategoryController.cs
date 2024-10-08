using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechCommerce.Models;
using TechCommerce.Repositories;


namespace TechCommerce.Controllers
{
[Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IGenericRepository<Category> CategoryRepository;
        private readonly IGenericRepository<Product> ProductRepository;

        public CategoryController(IGenericRepository<Category> categoryRepository, IGenericRepository<Product> productRepository)  //Inject
        {
            CategoryRepository = categoryRepository;
            ProductRepository = productRepository;
        }

        // Read
        public IActionResult Index(string searchQuery = "", int pg = 1)
        {
            List<Category> categories = CategoryRepository.GetAll();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                categories = categories.Where(c => c.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            const int pageSize = 8;
            if (pg < 1)
                pg = 1;

            var pager = new Pager(categories.Count, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            List<Category> filteredCategories = categories.Skip(recSkip).Take(pager.PageSize).ToList();

            CategoriesViewModel viewModel = new CategoriesViewModel()
            {
                Categories = filteredCategories,
                pagerViewModel = new PagerViewModel()
                {
                    Pager= pager,
                    SearchQuery= searchQuery
                }
            };

            return View("Index", viewModel);
        }

        public IActionResult ShowDetails(int id)
        {
            Category? category = CategoryRepository.GetById(id);

            Category categoryObj = new Category()
            {
                Id = category.Id,
                Name = category.Name,
                Products = ProductRepository.GetAll().Where(product => product.CategoryId == id).ToList()
            };

            return category != null ? View("ShowDetails", categoryObj) : NotFound();
        }


        // Create
        public IActionResult Add()
        {
            return View("Add");
        }

        public IActionResult SaveAdd(Category category)
        {
            if (ModelState.IsValid)
            {
                CategoryRepository.Add(category);
                CategoryRepository.Save();
                return RedirectToAction("Index");
            }
            return View("Add", category);
        }

        // Update
        public IActionResult Edit(int id)
        {
            Category? category = CategoryRepository.GetById(id);

            if (category != null)
            {

                return View("Edit", category);
            }
            else
            {
                return NotFound();
            }
        }

        public ActionResult SaveEdit(Category category, int id)
        {
            if (ModelState.IsValid)
            {
                Category existingCategory = CategoryRepository.GetById(id);

                if (existingCategory != null)
                {
                    existingCategory.Name = category.Name;
                    CategoryRepository.Update(existingCategory);
                    CategoryRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            return View("Edit", category);
        }

        // Delete
        public ActionResult Delete(int id)
        {
            Category? category = CategoryRepository.GetById(id);

            if (category != null)
            {
                CategoryRepository.Delete(id);
                CategoryRepository.Save();
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}
