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
                
            }
            _context.Update(query);
            UpdateSumToPay(query.TotalPrice);
            await _context.SaveChangesAsync();
            Object obj = 5;
            return RedirectToAction(nameof(CartDetails));
        }
        
        public async Task<IActionResult> Minus(int id)
        {
            var query = await _context.CartItem.Include(p => p.Product).FirstOrDefaultAsync(s => s.Id == id);
            if (query != null)
            {
                query.Quantity -= 1;
                query.TotalPrice = query.Product.Price * query.Quantity;
                UpdateSumToPay(query.TotalPrice);
            }
            _context.Update(query);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(CartDetails));
        }

        public async void UpdateSumToPay(double price)
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "Name")?.Value;
            var cart = await _context.Cart.Where(s => (s.Account.Name == user) && (s.Order == null)).FirstOrDefaultAsync<Cart>();
            cart.SumToPay += price;
            _context.Update(cart);
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

                id = _context.Cart.Where(s => s.Account.Name == user).FirstOrDefault().Id;
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

        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.Account)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
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

            var query = _context.Cart.Where(s => (s.Account.Name == user) && (s.Order == null)).FirstOrDefault<Cart>();
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
                     UpdateSumToPay(cartItem.TotalPrice);
                    //query.SumToPay += cartItem.TotalPrice;
                    //_context.Update(query);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                for(int i=0; i<quantity; i++)
                {
                    await Plus(c.Id);
                }
                //query.SumToPay += c.TotalPrice;
                //_context.Update(query);
                await _context.SaveChangesAsync();
            }    

            ViewBag.CartCount = query.CartItems.Count();
            return RedirectToAction("ProductDetails","Products", new { id = Int32.Parse(productId) });
        }

        // GET: Carts/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.Account, "Id", "Id");
            return View();
        }

        // POST: Carts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountId")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Account, "Id", "Id", cart.AccountId);
            return View(cart);
        }

        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.Account, "Id", "Id", cart.AccountId);
            return View(cart);
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountId")] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CartExists(cart.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Account, "Id", "Id", cart.AccountId);
            return View(cart);
        }

        // GET: Carts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart
                .Include(c => c.Account)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cart == null)
            {
                return NotFound();
            }

            return View(cart);
        }

        // POST: Carts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cart = await _context.Cart.FindAsync(id);
            _context.Cart.Remove(cart);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.Id == id);
        }
    }
}
