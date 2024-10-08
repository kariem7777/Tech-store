using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;
using TechCommerce.Data;
using TechCommerce.Models;
using TechCommerce.Repositories;
using Address = TechCommerce.Models.Address;
using Customer = TechCommerce.Models.Customer;
using Product = TechCommerce.Models.Product;

namespace TechCommerce.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext Context;
        private readonly UserManager<Customer> Usrmanager;
        private readonly IGenericRepository<Order> OrderRepository;
        private readonly IGenericRepository<Product> ProductRepository;
        private readonly IGenericRepository<Cart> CartRepository;
        private readonly ICartProductsRepository CartProductsRepository;
        private readonly IOrderProductRepository OrderProductRepository;
        private readonly IGenericRepository<Customer> CustomerRepository;

        public OrderController(IGenericRepository<Order> OrderRepository, ApplicationDbContext context, 
                               UserManager<Customer> Usrmanager,
                               IGenericRepository<Cart> CartRepository,
                               IOrderProductRepository OrderProductRepository,
                               ICartProductsRepository CartProductsRepository,
                               IGenericRepository<Product> ProductRepository,
                               IGenericRepository<Customer> CustomerRepository)  //Inject
        {
            this.OrderRepository = OrderRepository;
            Context = context;
            this.Usrmanager = Usrmanager;
            this.CartRepository = CartRepository;
            this.OrderProductRepository = OrderProductRepository;
            this.CartProductsRepository = CartProductsRepository;
            this.ProductRepository = ProductRepository;
            this.CustomerRepository = CustomerRepository;
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

            List<OrderDetailsViewModel> ordersList = OrderProductRepository.GetOrderDetails(order.Id);

            ViewBag.totalPrice = order.Price;
            return View("ShowDetails", ordersList);
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

            Customer user = CustomerRepository.GetById(userId);
            
            List<Address> addresses = Context.Addresses?.Where(A => A.CustomerId == user.Id).ToList() ?? new List<Address>();

            return View(addresses);
        }
        public async Task<IActionResult> PlaceOrder(int id,String mainst,String City)
        {
           
            var userId = Usrmanager.GetUserId(User);

            Customer user = CustomerRepository.GetById(userId);

            Cart c = CartRepository.GetById(user.CartId);

            List<CartProducts> cartproducts = CartProductsRepository.GetCartProducts(user.CartId);

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
                OrderProductRepository.Add(entry);
                CartProductsRepository.RemoveProduct(item);
               
                // Update the units in the stock 
                Product productDb = ProductRepository.GetById(item.ProductId);
                productDb.Units -= item.Quantity;
                ProductRepository.Update(productDb);
                ProductRepository.Save();
            }
            c.totalPrice = 0;
            CartProductsRepository.Save();
            OrderProductRepository.Save();

            TempData["success"] = "Order Placed Successfully";
            ViewBag.address= $"{mainst} | {City}";
            return View(newOrder);
        }

        
        public ActionResult PaymentMethod(int id, String mainst, String City)
        {
            ViewBag.id = id;
            ViewBag.mainst= mainst;
            ViewBag.City= City;
            return View();
        }

        [HttpPost]
        public ActionResult PaymentMethod(int id, String mainst, String City, String paymentmethod)
        {
            
            if (paymentmethod == "Cash")
            {
                return RedirectToAction(nameof(PlaceOrder), new { id = id, mainst = mainst, city = City });
            }
            else
            {
                return RedirectToAction(nameof(CreateCheckoutSession), new { id = id, mainst = mainst, city = City });
              
            }
        }

       
        public async Task<IActionResult> CreateCheckoutSession(int id, String mainst, String City)
        {
            var domain = "http://localhost:27980/";
            var userId = Usrmanager.GetUserId(User);

            Customer user = CustomerRepository.GetById(userId);

            Cart c = CartRepository.GetById(user.CartId);

            List<CartProducts> cartproducts = CartProductsRepository.GetCartProducts(user.CartId);

            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),
                
                Mode = "payment",
                SuccessUrl =domain+ $"Order/PlaceOrder?id={id}&mainst={mainst}&City={City}",
                CancelUrl = domain + $"Order/CheckOut",
            };

            foreach(var item in cartproducts)
            {

                var sessionitem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Product.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.Name,
                        },
                    },
                    Quantity = item.Quantity,
                };
                options.LineItems.Add(sessionitem);
             
            }
            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }
    }
}
