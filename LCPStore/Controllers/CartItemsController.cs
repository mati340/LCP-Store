using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LCPStore.Data;
using LCPStore.Models;

namespace LCPStore.Controllers
{
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


        private bool CartItemExists(int id)
        {
            return _context.CartItem.Any(e => e.Id == id);
        }
    }
}
