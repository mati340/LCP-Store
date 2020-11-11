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

            var user = User.Claims.FirstOrDefault(c => c.Type == "Name")?.Value;
            if (user == null)
            {
                return RedirectToAction("Login", "Accounts");
            }

            ViewBag.Categories = new ArrayList(_context.Category.ToList());

            IEnumerable<Product> LetestProducts = _context.Product.ToList().TakeLast(5);
            ViewData["LetestProducts"] = LetestProducts;

            //Relevant Products Per User

            var categories =(from orderItem in _context.OrderItem
                             where (orderItem.Order.Account.Name == user)
                             select orderItem.Product.Category);
            
            if(categories!=null)
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

        public IActionResult Privacy()
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
