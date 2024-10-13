using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechCommerce.Data;
using TechCommerce.Models;

namespace TechCommerce.Controllers
{
    [Authorize]
    public class RatingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Customer> Usrmanager;
        public RatingController(ApplicationDbContext context,UserManager<Customer> userManager)
        {
            _context = context;
            Usrmanager = userManager;
        }

        [HttpGet]
        public IActionResult GetUnratedProducts()
        {
          
            var usrId = Usrmanager.GetUserId(User);
            var unratedProducts = _context.Orders
                .Where(o => o.CustomerId == usrId && o.State == "Delivered")
                .SelectMany(o => o.OrPr)
                .Where(p => !p.Product.Prate.Any(r => r.UserId == usrId))
                .Select(p => new { p.ProductId,p.Product.Name,p.Product.ImageUrl, UserId = usrId })
                .ToList();

            return Json(unratedProducts);
        }

        [HttpPost]
        public  async Task<IActionResult> SubmitRating(ProductRate rating)
        {
            if (ModelState.IsValid)
            {
                var usrId = Usrmanager.GetUserId(User); 
                var user = await Usrmanager.FindByIdAsync(usrId);
                rating.CustomerName = user.Firstname;

                _context.Rates.Add(rating);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest(ModelState);
        }
    }
}
