using TechCommerce.Models;

namespace TechCommerce.ViewModel
{
    public class ProductsSearchPagerViewModel
    {
        public string SearchQuery { get; set; } = "";
        public Pager? Pager { get; set; }

        public List<Product> products = [];

        public int CategoryId { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
