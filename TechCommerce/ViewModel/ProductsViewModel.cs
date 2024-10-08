using TechCommerce.Models;

namespace TechCommerce.ViewModel
{
    public class ProductsViewModel
    {
        public PagerViewModel pagerViewModel { get; set; }

        public List<Product> products = [];

        public int CategoryId { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
