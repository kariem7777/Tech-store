using TechCommerce.Models;

namespace TechCommerce.ViewModel
{
    public class CategoriesSearchPagerViewModel
    {
        public string SearchQuery { get; set; } = "";
        public Pager? Pager { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
