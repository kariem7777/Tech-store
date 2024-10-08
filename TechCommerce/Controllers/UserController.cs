using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechCommerce.Models;
using TechCommerce.Repositories;

namespace TechCommerce.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<Customer> usrmngr;
        private readonly IGenericRepository<Address> AddressRepository;

        public UserController(UserManager<Customer> usrmngr, IGenericRepository<Address> addrRepo)
        {
            this.usrmngr = usrmngr;
            AddressRepository = addrRepo;
        }
        public IActionResult AddAddress(string returnUrl = null)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        public IActionResult AddAddress(Address A, string returnUrl = null)
        {
            ModelState.Remove(nameof(A.CustomerId));
            ModelState.Remove(nameof(A.Customer));

            if (!ModelState.IsValid)
            {
                ViewBag.returnUrl = returnUrl;
                return View();
            }
            var userId = usrmngr.GetUserId(User);
            A.CustomerId = userId;
            AddressRepository.Add(A);
            AddressRepository.Save();
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
