using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TechCommerce.Data;
using TechCommerce.Models;
using TechCommerce.Repositories;

namespace TechCommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly IRepository<Cart> CartRepository;
        private readonly IRepository<CartProducts> CartProductRepository;
        private readonly IRepository<Product> ProductRepository;
        private readonly UserManager<Customer> UserManager;
        private readonly ApplicationDbContext Context;

        public CartController(IRepository<Cart> CartRepository, IRepository<CartProducts> CartProductRepository, IRepository<Product> ProductRepository, UserManager<Customer> UserManager, ApplicationDbContext Context)  // Update to Customer
        {
            this.CartRepository = CartRepository;
            this.CartProductRepository = CartProductRepository;
            this.ProductRepository = ProductRepository;
            this.UserManager = UserManager;
            this.Context = Context;
        }

        public async Task<IActionResult> Index()
        {
            var userId = UserManager.GetUserId(User);

            var user = await Context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            Cart mycart = CartRepository.GetById(user.CartId);

            ViewBag.CartTotalPrice = mycart.totalPrice;

            List<CartProductViewModel> cartProducts = await Context.CartProducts
            .Include(cp => cp.Product)
            .Where(cp => cp.CartId == user.CartId)
            .Select(cp => new CartProductViewModel
            {
                ProductId = cp.ProductId,
                Name = cp.Product.Name ?? "Unknown",
                Units = cp.Product.Units,
                Price = cp.Product.Price,
                Description = cp.Product.Description ?? "No description",
                ImageUrl = cp.Product.ImageUrl ?? string.Empty,
                CategoryId = cp.Product.CategoryId,
                Quantity = cp.Quantity
            })
            .ToListAsync();


            return View(cartProducts);
        }

        [Authorize]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
        {
            var userId = UserManager.GetUserId(User);

            var customer = await Context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            var product = ProductRepository.GetById(productId);

            var cartProduct = CartProductRepository.GetAll()
                .FirstOrDefault(cp => cp.CartId == customer.CartId && cp.ProductId == productId);

            if (cartProduct != null)
            {
                // If the product is already in the cart, increase the quantity
                cartProduct.Quantity += quantity;
                CartProductRepository.Update(cartProduct);
            }
            else
            {
                var newCartProduct = new CartProducts
                {
                    CartId = customer.CartId,
                    ProductId = product.Id,
                    Quantity = quantity
                };
                CartProductRepository.Add(newCartProduct);
            }

            Cart cart = CartRepository.GetById(customer.CartId);
            cart.totalPrice += product.Price * quantity;
            CartRepository.Update(cart);

            CartProductRepository.Save();

            return NoContent();
        }



        public async Task<IActionResult> IncrementToCart(int productId)
        {
            var userId = UserManager.GetUserId(User);

            var customer = await Context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            var product = ProductRepository.GetById(productId);

            var cartProduct = CartProductRepository.GetAll()
                .FirstOrDefault(cp => cp.CartId == customer.CartId && cp.ProductId == productId);

            if (cartProduct != null)
            {
                // If the product is already in the cart, increase the quantity
                cartProduct.Quantity += 1;
                CartProductRepository.Update(cartProduct);
            }

            Cart cart = CartRepository.GetById(customer.CartId);
            cart.totalPrice += product.Price;
            CartRepository.Update(cart);

            CartProductRepository.Save();

            return NoContent();
        }

        public async Task<IActionResult> DecrementToCart(int productId)
        {
            var userId = UserManager.GetUserId(User);

            var customer = await Context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            var product = ProductRepository.GetById(productId);

            var cartProduct = CartProductRepository.GetAll()
                .FirstOrDefault(cp => cp.CartId == customer.CartId && cp.ProductId == productId);

            if (cartProduct.Quantity > 1)
            {
                if (cartProduct != null)
                {
                    // If the product is already in the cart, increase the quantity
                    cartProduct.Quantity -= 1;
                    CartProductRepository.Update(cartProduct);
                }

                Cart cart = CartRepository.GetById(customer.CartId);
                cart.totalPrice -= product.Price;
                CartRepository.Update(cart);

                CartProductRepository.Save();
            }

            return NoContent();
        }

        public ActionResult RemoveFromCart(int productId)
        {
            var userId = UserManager.GetUserId(User);

            var customer = Context.Users
                .Where(u => u.Id == userId)
                .FirstOrDefault();

            var product = ProductRepository.GetById(productId);

            var cartProduct = CartProductRepository.GetAll()
                .FirstOrDefault(cp => cp.CartId == customer.CartId && cp.ProductId == productId);

            if (cartProduct != null)
            {
                CartProductRepository.RemoveProduct(cartProduct);
                CartProductRepository.Save();
            }

            Cart cart = CartRepository.GetById(customer.CartId);
            cart.totalPrice -= product.Price * cartProduct.Quantity;
            CartRepository.Update(cart);
            CartProductRepository.Save();

            return RedirectToAction("Index");
        }

    }
}