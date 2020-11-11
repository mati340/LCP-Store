using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LCPStore.Data;
using LCPStore.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading;
using System.Security.Claims;

namespace LCPStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly LCPStoreContext _context;

        public OrdersController(LCPStoreContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Order.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (user == null)
            {
                return RedirectToAction("Login", "Accounts");
            }

            Cart cart = _context.Cart.Where(s => s.Account.Username == user).Include(i=>i.CartItems).ThenInclude(p=>p.Product).First();
            foreach(CartItem ci in cart.CartItems)
            {
                if(ci.Quantity == 0)
                {
                    _context.CartItem.Remove(ci);
                }
            }
            _context.SaveChanges();
            ViewData["cart_to_view"] = cart;


            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("Id,Country,City,Address,ZipCode,PhoneNumber,TotalPay,Delivery,OrderTime")] Order order)
        {

            var user = User.Claims.FirstOrDefault(c => c.Type == "Name")?.Value;
            if (user == null)
            {
                return RedirectToAction("Login", "Accounts");
            }

            Cart cart = await _context.Cart.Where(s => s.Account.Name == user)
                .Include(c=>c.CartItems).ThenInclude(p=>p.Product)
                .Include(a=>a.Account)
                .FirstOrDefaultAsync<Cart>();

            if (ModelState.IsValid)
            {
                order.OrderTime = DateTime.Now;
                order.Account = cart.Account;
                order.OrderItems = new List<OrderItem>();
                if (order.Delivery.ToString() == "Standart") 
                {
                    order.TotalPay = cart.SumToPay;
                }
                else 
                { order.TotalPay = cart.SumToPay + 3; }
                
                foreach (var item in cart.CartItems)
                {
                    if(item.Quantity>0)
                    {
                        order.OrderItems.Add(new OrderItem() { Order = order, Product = item.Product, Quantity = item.Quantity, TotalPrice = item.TotalPrice });
                    }
                }
                _context.Add(order);
                cart.Order = order;
                _context.CartItem.RemoveRange(_context.CartItem.Where(c=>c.Cart == cart));
                cart.SumToPay = 0;
                cart.Order = null;
                await _context.SaveChangesAsync();

                String orderID = order.Id.ToString();
                ViewData["Message"] = orderID;
                return View("Ordered");
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Country,City,Address,ZipCode,PhoneNumber,TotalPay,Delivery,OrderTime")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Order.FindAsync(id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
