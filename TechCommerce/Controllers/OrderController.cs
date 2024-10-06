using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TechCommerce.Data;
using TechCommerce.Models;
using TechCommerce.Repositories;

namespace TechCommerce.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IRepository<Order> OrderRepository;
        private readonly ApplicationDbContext Context;
        private readonly UserManager<Customer> Usrmanager;
        private readonly IRepository<Cart> CartRepository;
        private readonly IRepository<CartProducts> CartprRepository;
        private readonly IRepository<Address> AddressRepository;
        private readonly IRepository<OrderProduct> OrderPrRepository;
        public OrderController(IRepository<Order> OrderRepo,ApplicationDbContext context, UserManager<Customer> um, IRepository<Address> AddRepository, IRepository<Cart> CartRepository,
            IRepository<OrderProduct> OrderPrRepository, IRepository<CartProducts> CartprRepository)  //Inject
        {
            OrderRepository = OrderRepo;
            Context = context;
            Usrmanager = um;
            this.CartRepository = CartRepository;
            AddressRepository = AddRepository;
            this.OrderPrRepository = OrderPrRepository;
            this.CartprRepository = CartprRepository;
        }

        // Read
        public IActionResult Index(int pg = 1)
        {
            List<Order> orders;

            if (User.IsInRole("Admin"))
            {
                orders = OrderRepository.GetAll();
            }
            else
            {
                var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                orders = OrderRepository.GetAll().Where(p => p.CustomerId == customerId).ToList();
            }

            const int pageSize = 8;
            if (pg < 1)
                pg = 1;

            var pager = new Pager(orders.Count, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            List<Order> filteredOrders = orders.Skip(recSkip).Take(pager.PageSize).ToList();

            OrdersPagertViewModel ordersPagertViewModel = new OrdersPagertViewModel()
            {
                orders = filteredOrders,
                Pager = pager,
            };

            return View("Index", ordersPagertViewModel);
        }

        public IActionResult ShowDetails(int id)
        {
            Order? order = OrderRepository.GetById(id);
            List<OrderDetailsViewModel> products=  Context.OrderProduct.Include(O=>O.Product).Where(O => O.OrderId == id)
                .Select(O=>new OrderDetailsViewModel
            {
                Quantity=O.Quantity,
                Price=O.Price,
                Name = O.Product.Name
            }).ToList();

            ViewBag.totalPrice = order.Price;
            return View("ShowDetails", products);
        }

        // U
        public IActionResult Edit(int id)
        {
            Order? order = OrderRepository.GetById(id);

            if (order != null)
            {
                return View("Edit", order);
            }
            else
            {
                return NotFound();
            }
        }

        public ActionResult SaveEdit(Order order, int id)
        {
            Order? orderssDB = OrderRepository.GetById(id);

            if (orderssDB != null)
            {
                orderssDB.State = order.State;

                OrderRepository.Update(orderssDB);
                OrderRepository.Save();

                return RedirectToAction("Index");
            }

            return View("Edit", order);
        }

        // Delete
        public ActionResult Delete(int id)
        {
            Order? order = OrderRepository.GetById(id);

            OrderRepository.Delete(id);
            OrderRepository.Save();

            return RedirectToAction("Index");
        }
        [Authorize]
        public async Task<IActionResult> CheckOut() 
        {
            var userId = Usrmanager.GetUserId(User);

            var user = await Context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            //List<CartProducts> cartproducts = CartprRepository.GetbyIDWithProducts(user.CartId); 
            List<Address> addresses = Context.Addresses?.Where(A => A.CustomerId == user.Id).ToList()?? new List<Address>();



            return View(addresses);
        }
        public async Task<IActionResult> PlaceOrder(int id,String mainst,String City)
        {
            var userId = Usrmanager.GetUserId(User);

            var user = await Context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();
            Cart c = CartRepository.GetById(user.CartId);

            List<CartProducts> cartproducts = Context.CartProducts.Include(cp => cp.Product).Where(cp => cp.CartId == user.CartId).ToList();
            Order newOrder = new Order()
            {
                Price = c.totalPrice,
                CustomerId = userId,
                State="Placed",
                Customer=user,
                Date=DateTime.Now,
                AddressId=id
                
            };
            OrderRepository.Add(newOrder);
            OrderRepository.Save();
            foreach(var item in cartproducts)
            {
                OrderProduct entry= new OrderProduct
                {
                    OrderId = newOrder.Id,
                    Order = newOrder,
                    ProductId = item.ProductId,
                    Product = item.Product,
                    Quantity = item.Quantity,
                    Price=item.Product.Price
                 
                    
                };
                OrderPrRepository.Add(entry);
                CartprRepository.RemoveProduct(item);
            }
            OrderPrRepository.Save();
            TempData["success"] = "Order Placed Successfully";
            ViewBag.address= $"{mainst} | {City}";
            return View(newOrder);
        }

    }
}
