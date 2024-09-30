using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using TechCommerce.Models;
using TechCommerce.Repositories;

namespace TechCommerce.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        IRepository<Order> OrderRepository;

        public OrderController(IRepository<Order> OrderRepo)  //Inject
        {
            OrderRepository = OrderRepo;
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

            return order != null ? View("ShowDetails", order) : NotFound();
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
    }
}
