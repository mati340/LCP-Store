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

namespace LCPStore.Controllers
{
    [Authorize]
    public class CartItemsController : Controller
    {
        private readonly LCPStoreContext _context;

        public CartItemsController(LCPStoreContext context)
        {
            _context = context;
        }

        // GET: CartItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.CartItem.ToListAsync());
        }

        // POST: CartItems/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<double> Delete(int id)
        {
            var cartItem = await _context.CartItem.Include(c => c.Cart).FirstOrDefaultAsync(c => c.Id == id);
            cartItem.Cart.SumToPay -= cartItem.TotalPrice;
            _context.CartItem.Remove(cartItem);
            await _context.SaveChangesAsync();
            return cartItem.Cart.SumToPay;
        }

        // POST: CartItems/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<double[]> Update(int id, int quantity)
        {
            var cartItem = await _context.CartItem.Include(c => c.Cart).Include(p => p.Product).FirstOrDefaultAsync(c => c.Id == id);

            cartItem.Quantity += quantity;
            cartItem.Cart.SumToPay -= cartItem.TotalPrice;
            cartItem.TotalPrice += (cartItem.Product.Price * quantity);
            cartItem.Cart.SumToPay += cartItem.TotalPrice;

            await _context.SaveChangesAsync();

            double[] arr = { cartItem.TotalPrice, cartItem.Cart.SumToPay };
            return arr;
        }

        private bool CartItemExists(int id)
        {
            return _context.CartItem.Any(e => e.Id == id);
        }
    }
}
