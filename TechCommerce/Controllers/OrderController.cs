using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TechCommerce.Models;
using TechCommerce.Repositories;

namespace TechCommerce.Controllers
{
    public class OrderController : Controller
    {
        IRepository<Order> OrderRepository;
        IRepository<Customer> CustomerRepository;

        public OrderController(IRepository<Order> OrderRepo, IRepository<Customer> customerRepository)  //Inject
        {
            OrderRepository = OrderRepo;
            CustomerRepository = customerRepository;
        }

        // Read
        public IActionResult Index(int customerId = 0, int pg = 1)
        {
            List<Order> orders = OrderRepository.GetAll();

            if (customerId > 0)
            {
                orders = orders.Where(p => p.CustomerId == customerId).ToList();
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
                CustomerId = customerId,
                Customers = CustomerRepository.GetAll()
            };

            return View("Index", ordersPagertViewModel);
        }

        public IActionResult ShowDetails(int id)
        {
            Order? order = OrderRepository.GetById(id);

            return order != null ? View("ShowDetails", order) : NotFound();
        }

        // U
        public IActionResult Edit(int id)
        {
            Order? order = OrderRepository.GetById(id);

            if (order != null)
            {
                OrderViewModel orderViewModel = new OrderViewModel
                {
                    Id = id,
                    Price = (int?)order.Price,
                    Date = order.Date,
                    State = order.State,
                    CustomerId = order.CustomerId,
                    Customers = CustomerRepository.GetAll()                
                };

                return View("Edit", orderViewModel);
            }
            else
            {
                return NotFound();
            }
        }


        public ActionResult SaveEdit(OrderViewModel orderViewModel, int id)
        {
            if (ModelState.IsValid)
            {
                Order? orderssDB = OrderRepository.GetById(id);

                if (orderssDB != null)
                {
                    orderssDB.Price = (int)orderViewModel.Price;
                    orderssDB.Date = (DateTime)orderViewModel.Date;
                    orderssDB.CustomerId = orderViewModel.CustomerId;
                    orderssDB.State = orderViewModel.State;

                  
                    OrderRepository.Update(orderssDB);
                    OrderRepository.Save();

                    return RedirectToAction("Index");
                }

            }
            orderViewModel.Customers = CustomerRepository.GetAll();
            return View("Edit", orderViewModel);
        }


        // Delete
        public ActionResult Delete(int id)
        {
            Order? order = OrderRepository.GetById(id);

            OrderRepository.Delete(id);
            OrderRepository.Save();

            return RedirectToAction("Index");
        }

    }
}
