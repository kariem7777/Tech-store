using Microsoft.AspNetCore.Mvc;
using TechCommerce.Models;

namespace TechCommerce.ViewModel
{
    public class CategoriesViewModel
    {
        public PagerViewModel pagerViewModel { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
