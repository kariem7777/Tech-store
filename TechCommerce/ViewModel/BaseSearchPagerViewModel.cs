using TechCommerce.Models;

namespace TechCommerce.ViewModel
{
    public class BaseSearchPagerViewModel
    {
        public string SearchQuery { get; set; } = "";
        public Pager? Pager { get; set; }

        public string ControllerName { get; set; }
    }
}
