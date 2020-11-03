using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LCPStore.Data;
using LCPStore.Models;
using Microsoft.CodeAnalysis;

//dffdvf
namespace LCPStore.Controllers
{
    public class CartsController : Controller
    {
        private readonly LCPStoreContext _context;

        public CartsController(LCPStoreContext context)
        {
            _context = context; 
        }
        public async Task<IActionResult> Plus(int id)
        {
            var query = await _context.CartItem.Include(p=>p.Product).FirstOrDefaultAsync(s => s.Id == id);
            if (query != null)
            {
                query.Quantity += 1;
                query.TotalPrice = query.Product.Price * query.Quantity;
                await _context.SaveChangesAsync();
                await UpdateSumToPay(query.Product.Price);
            }
            return RedirectToAction(nameof(CartDetails));
        }
        
        public async Task<IActionResult> Minus(int id)
        {
            var query = await _context.CartItem.Include(p => p.Product).FirstOrDefaultAsync(s => s.Id == id);
            if (query != null)
            {
                query.Quantity -= 1;
                query.TotalPrice = query.Product.Price * query.Quantity;
                await _context.SaveChangesAsync();
                await UpdateSumToPay(-query.Product.Price);
            }
            return RedirectToAction(nameof(CartDetails));
        }

        public async Task UpdateSumToPay(double price)
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Name")?.Value;
            var cart = await _context.Cart.FirstOrDefaultAsync(s => s.Account.Name == user);
            //if cart==null  TODO
            cart.SumToPay += price;
            await _context.SaveChangesAsync();
        }

        // GET: Carts/Details/1
        public async Task<IActionResult> CartDetails(int? id)
        {
            if (id == null)
            {
                var user = User.Claims.FirstOrDefault(c => c.Type == "Name")?.Value;
                if (user == null)
                {
                    return RedirectToAction("Login", "Accounts");
                }

                var c = _context.Cart.Where(s => s.Account.Name == user).FirstOrDefault();
                if (c == null)
                    return NotFound();
                id = c.Id;

                  
            }

            var cart = await _context.Cart
                .Include(c => c.Account)
                .Include(ci => ci.CartItems)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var lCPStoreContext = _context.Cart.Include(c => c.Account);
            return View(await lCPStoreContext.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Add To Cart
        public async Task<IActionResult> AddToCart(string productId, int quantity=1)
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Name")?.Value;
            if (user == null)
            {
                return RedirectToAction("Login", "Accounts");
            }

            var query = _context.Cart.Where(s => s.Account.Name == user).FirstOrDefault<Cart>();
            if (query == null)
            {
                Account account = _context.Account.First(s => s.Name == user);
                Cart cart = new Cart { Account = account, AccountId = account.Id , SumToPay=0};
                _context.Cart.Add(cart);
                await _context.SaveChangesAsync();
                query = cart;
            }

            var c = _context.CartItem.Where(s => s.Cart == query).Where(p => p.Product.Id.ToString() == productId).FirstOrDefault<CartItem>();
            if (c == null)
            {
                CartItem cartItem = new CartItem();
                cartItem.Quantity = quantity;
                var product = await _context.Product.FirstOrDefaultAsync(s => s.Id.ToString() == productId);
                cartItem.Product = product;
                cartItem.TotalPrice = product.Price * cartItem.Quantity;
                cartItem.Cart = query;
                if (ModelState.IsValid)
                {
                    _context.Add(cartItem);
                    await _context.SaveChangesAsync();
                    await UpdateSumToPay(cartItem.TotalPrice);
                }
            }
            else
            {
                for(int i=0; i<quantity; i++)
                {
                    await Plus(c.Id);
                }
            }    

            return RedirectToAction("ProductDetails","Products", new { id = Int32.Parse(productId) });
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.Id == id);
        }
        //public ActionResult CountItems()
        //{
        //    var user = User.Claims.FirstOrDefault(c => c.Type == "Name")?.Value;
        //    if (user == null)
        //    {
        //        //return RedirectToAction("Login", "Accounts");
        //    }

        //    var query = _context.Cart.Include(c=>c.CartItems).FirstOrDefault(s => s.Account.Name == user);

        //    var model = query.CartItems.ToList().Count();
        //    return PartialView("~/Views/Shared/_Layout.cshtml", model.ToString());
        //}
    }
}
