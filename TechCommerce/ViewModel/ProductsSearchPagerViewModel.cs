using TechCommerce.Models;

namespace TechCommerce.ViewModel
{
    public class ProductsSearchPagerViewModel : BaseSearchPagerViewModel
    {
        public List<Product> products = [];

        public int CategoryId { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();

        public ProductsSearchPagerViewModel()
        {
            ControllerName = "Product";
        }
    }
}
