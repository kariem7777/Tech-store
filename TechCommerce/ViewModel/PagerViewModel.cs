using TechCommerce.Models;

namespace TechCommerce.ViewModel
{
    public class PagerViewModel
    {
        public string SearchQuery { get; set; } = "";
        public Pager? Pager { get; set; }
    }
}
