using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LCPStore.Data;
using LCPStore.Models;
using System.IO;
using System.Threading;

namespace LCPStore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly LCPStoreContext _context;

        public ProductsController(LCPStoreContext context)
        {
            _context = context;
        }

        // GET: Products/Details/5
        public async Task<IActionResult> ProductDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // Search-Auto Complete
        public async Task<IActionResult> SearchAuto(string term)
        {
            var query = from p in _context.Product
                        where p.Name.ToLower().Contains(term.ToLower())
                        select new { id = p.Id, label = p.Name, value = p.Id };

            return Json(await query.ToListAsync());
        }
        
        public async Task<IActionResult> RelatedProducts(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var q = (from c in _context.Category
            //         where c.Id == id
            //         orderby a.Created descending
            //         select new ICollection<Product> { }).Take(5);
            Category category = await _context.Category.FirstOrDefaultAsync(c => c.Id == id);
            var products = category.Products.Take(5);
            //ICollection<Product> relproduct = await q.ToListAsync();

            return View(products);
        }
        
        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
