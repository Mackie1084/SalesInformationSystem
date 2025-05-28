using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SalesInformationSystem.Data;
using SalesInformationSystem.Models;


namespace SalesInformationSystem.Controllers
{
    [Authorize(Roles ="Customer")]
    public class ShoppingCartItemsController : Controller
    {
        public string ShoppingCartId { get; set; }
        public const string CartSessionKey="CartId";

        private readonly ApplicationDbContext _context;
       


        public ShoppingCartItemsController(ApplicationDbContext context)
        {
            this.ShoppingCartId = "";
            _context = context;
          
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            ShoppingCartId = GetCartId();
            CartItems cartItem = new CartItems();
            cartItem = _context.CartItems.SingleOrDefault(c => c.CartId == ShoppingCartId && c.ProductId == id);
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists.
                cartItem = new CartItems
                {
                    ItemId = id,
                    ProductId = id,
                    Price = (int)_context.Product.SingleOrDefault(p => p.ProductId == id).ProductPrice,
                    CartId = ShoppingCartId,
                    Product = _context.Product.SingleOrDefault(p => p.ProductId == id),
                    Quantity = 1,
                    DateCreated = DateTime.Now
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart,
                // then add one to the quantity.
                cartItem.Quantity++;
            }
           await _context.SaveChangesAsync();
            return RedirectToAction("DisplayCartItems");
        }

        public async Task<IActionResult> DeleteCartItem(int id)
        {
            var cartItemDelete = _context.CartItems.Find(id);

            if (cartItemDelete != null)
            {
                _context.CartItems.Remove(cartItemDelete);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(DisplayCartItems));
        }

        public string GetCartId()
        {
            var session = HttpContext.Session.GetString(CartSessionKey);
            if (session == null)
            {
                if (!string.IsNullOrWhiteSpace(User.Identity.Name))
                {
                    session = User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class.
                    Guid tempCartId = Guid.NewGuid();
                    session = tempCartId.ToString();
                }
            }
            return session.ToString();
        }

        public List<CartItems> GetCartItems()
        {
            ShoppingCartId = GetCartId();
            return _context.CartItems.Where(
            c => c.CartId == ShoppingCartId).ToList();
        }
        public IActionResult DisplayCartItems()
        {
            var cartitems = GetCartItems();
            ViewBag.count = cartitems.Count;
            return View(cartitems);
        }

        public async Task<IActionResult> CheckOut()
        {
            var username = User.Identity.Name;
            var cartItems = _context.CartItems.Where(ci => ci.CartId == username).ToList();
            double TAmount = 0.0;

            foreach(var item in cartItems)
            {
                var total = item.Price * item.Quantity;
                TAmount = TAmount + total;
            }

            var paymentStatus = "Paid";
            var custId = _context.Customer.Where(c => c.Email == username).FirstOrDefault().CustomerId;
            Invoice inv = new Invoice();
            inv.InvoiceDate = DateOnly.FromDateTime(DateTime.Now);
            inv.TotalAmount = TAmount;
            inv.PaymentStatus = paymentStatus;
            inv.CustomerId = custId;
            _context.Invoice.Add(inv);

            await _context.SaveChangesAsync();

            return Redirect("/Payments/CheckOut");
        }

      
    }
}
