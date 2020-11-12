using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using LCPStore.Models;
using System.Collections;
using LCPStore.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace LCPStore.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly LCPStoreContext _context;

        public HomeController(ILogger<HomeController> logger, LCPStoreContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {

            var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            ViewBag.Categories = new ArrayList(_context.Category.ToList());

            IEnumerable<Product> LetestProducts = _context.Product.ToList().TakeLast(5);
            ViewData["LetestProducts"] = LetestProducts;

            //Relevant Products Per User

            var categories =(from orderItem in _context.OrderItem
                             where (orderItem.Order.Account.Username == user)
                             select orderItem.Product.Category).ToList();
            
            if(categories.Count > 1)
            {
                categories.ToList().Distinct().TakeLast(2);

                var RelevantProduct = (from p in _context.Product
                                       where ((p.Category == categories.ToArray()[0]) || (p.Category == categories.ToArray()[1]))
                                       select p);
                if (RelevantProduct != null)
                    RelevantProduct.ToList().Take(6);

                ViewData["RelevantProducts"] = RelevantProduct;
            }

            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
