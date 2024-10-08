using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TechCommerce.Models;
using TechCommerce.Repositories;

namespace TechCommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly IGenericRepository<Cart> CartRepository;
        private readonly ICartProductsRepository CartProductRepository;
        private readonly IGenericRepository<Product> ProductRepository;
        private readonly IGenericRepository<Customer> CustomerRepository;
        private readonly UserManager<Customer> UserManager;

        public CartController(IGenericRepository<Cart> CartRepository,
                                ICartProductsRepository CartProductRepository,
                                IGenericRepository<Product> ProductRepository,
                                IGenericRepository<Customer> CustomerRepository,
                                UserManager<Customer> UserManager)
        {
            this.CartRepository = CartRepository;
            this.CartProductRepository = CartProductRepository;
            this.ProductRepository = ProductRepository;
            this.UserManager = UserManager;
            this.CustomerRepository = CustomerRepository;
        }

        public async Task<IActionResult> Index()
        {
            var userId = UserManager.GetUserId(User);

            Customer user = CustomerRepository.GetById(userId);

            Cart mycart = CartRepository.GetById(user.CartId);

            ViewBag.CartTotalPrice = mycart.totalPrice;

            var cartProducts = await CartProductRepository.GetCartProductsAsync(user.CartId);

            return View(cartProducts);
        }

        [Authorize]
        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            var userId = UserManager.GetUserId(User);

            Customer customer = CustomerRepository.GetById(userId);

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
        public IActionResult IncrementToCart(int productId)
        {
            var userId = UserManager.GetUserId(User);

            Customer customer = CustomerRepository.GetById(userId);

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

        public IActionResult DecrementToCart(int productId)
        {
            var userId = UserManager.GetUserId(User);

            Customer customer = CustomerRepository.GetById(userId);

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

            Customer customer = CustomerRepository.GetById(userId);

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