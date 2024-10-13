using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechCommerce.Models;
using TechCommerce.Repositories;

namespace TechCommerce.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<Customer> usrmngr;
        private readonly IAddressRepository AddressRepository;

        public UserController(UserManager<Customer> usrmngr, IAddressRepository addrRepo)
        {
            this.usrmngr = usrmngr;
            AddressRepository = addrRepo;
        }
        public IActionResult AddUpdataAddress(int? id,string returnUrl = null)
        {
            ViewBag.returnUrl = returnUrl;
            if (id == null || id == 0) { 
                
                return View(new Address());
            }
            else
            {
                Address ad = AddressRepository.GetById(id.Value);
                return View(ad);
            }
        }
        [HttpPost]
        public IActionResult AddUpdataAddress(Address A, string returnUrl = null)
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
            if (A.Id == 0)
            {

                
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
            else
            {
                AddressRepository.Update(A);
                AddressRepository.Save();
                List<Address> addresses = AddressRepository.GetAllById(usrmngr.GetUserId(User));
                return View("ShowAddresses", addresses);
            }

        }
        public IActionResult ShowAddresses()
        {
            List<Address> addresses=AddressRepository.GetAllById(usrmngr.GetUserId(User));
            return View(addresses);
        }
        public IActionResult DeleteAddress(int id)
        {

            AddressRepository.Delete(id);
            AddressRepository.Save();
            List<Address> addresses = AddressRepository.GetAllById(usrmngr.GetUserId(User));
            return View("ShowAddresses", addresses);
        }


    }
}
