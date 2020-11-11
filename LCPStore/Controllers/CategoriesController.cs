using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LCPStore.Data;
using LCPStore.Models;
using System.Collections;

namespace LCPStore.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly LCPStoreContext _context;

        public CategoriesController(LCPStoreContext context)
        {
            _context = context;
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Store(string? id)
        {
            var category = new Category();
            ViewBag.Categories = new ArrayList(_context.Category.ToList());

            var isValidId = int.TryParse(id, out int categoryId);

            if (id == null)
            {
                category = await _context.Category
                    .Include(p => p.Products)
                    .FirstOrDefaultAsync();
            }
            else if (!isValidId)
            {
                throw new Exception("Id is not valid");
            }
            else
            {
                category = await _context.Category
                    .Include(p => p.Products)
                    .FirstOrDefaultAsync(m => m.Id == categoryId);
                if (category == null)
                {
                    return NotFound();
                }
            }

            return View(category.Products);
        }

        // Search By Price
        public async Task<IActionResult> SearchByPriceAndCategory(string minamount, string maxamount,String category)
        {
            int minim = Int32.Parse(minamount.Substring(1));
            int maxim = Int32.Parse(maxamount.Substring(1));
            var query = from p in _context.Product
                        where((p.Category.Id.ToString() == category) && (p.Price >= minim && p.Price <= maxim))
                        select p;

            return Json(await query.ToListAsync()); //TODO

        }


        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Category.ToListAsync());
        }


        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
    }
}
