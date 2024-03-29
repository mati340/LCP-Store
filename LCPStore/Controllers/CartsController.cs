﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LCPStore.Data;
using LCPStore.Models;
using Microsoft.CodeAnalysis;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace LCPStore.Controllers
{
    public class CartsController : Controller
    {
        private readonly LCPStoreContext _context;

        public CartsController(LCPStoreContext context)
        {
            _context = context; 
        }

        [Authorize]
        [HttpPost]
        public async Task<double[]> Plus(int id)
        {
            var query = await _context.CartItem.Include(p=>p.Product).FirstOrDefaultAsync(s => s.Id == id);
            if (query != null)
            {
                query.Quantity += 1;
                query.TotalPrice = query.Product.Price * query.Quantity;
                await _context.SaveChangesAsync();
                await UpdateSumToPay(query.Product.Price);
            }
            double[] arr = { query.TotalPrice, query.Cart.SumToPay };
            return arr;
        }

        [Authorize]
        [HttpPost]
        public async Task<double[]> Minus(int id)
        {
            var query = await _context.CartItem.Include(p => p.Product).FirstOrDefaultAsync(s => s.Id == id);
            if (query != null)
            {
                query.Quantity -= 1;
                query.TotalPrice = query.Product.Price * query.Quantity;
                await _context.SaveChangesAsync();
                await UpdateSumToPay(-query.Product.Price);
            }
            double[] arr = { query.TotalPrice, query.Cart.SumToPay };
            return arr;
        }

        [Authorize]
        public async Task UpdateSumToPay(double price)
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var cart = await _context.Cart.FirstOrDefaultAsync(s => s.Account.Username == user);
            //if cart==null  TODO
            cart.SumToPay += price;
            await _context.SaveChangesAsync();
        }

        [Authorize]
        // GET: Carts/Details/1
        public async Task<IActionResult> CartDetails(int? id)
        {
            if (id == null)
            {
                var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                if (user == null)
                {
                    return RedirectToAction("Login", "Accounts");
                }

                var c = _context.Cart.Where(s => s.Account.Username == user).FirstOrDefault();
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

        [Authorize]
        // GET: Carts
        public async Task<IActionResult> Index()
        {
            var lCPStoreContext = _context.Cart.Include(c => c.Account);
            return View(await lCPStoreContext.ToListAsync());
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        //Add To Cart
        public async Task<bool> AddToCart(int productId, int quantity=1)
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (user == null)
            {
                return false;
            }

            var query = _context.Cart.Where(s => s.Account.Username == user).FirstOrDefault<Cart>();
            if (query == null)
            {
                Account account = _context.Account.First(s => s.Name == user);
                Cart cart = new Cart { Account = account, AccountId = account.Id , SumToPay=0};
                _context.Cart.Add(cart);
                await _context.SaveChangesAsync();
                query = cart;
            }

            var c = _context.CartItem.Where(s => s.Cart == query).Where(p => p.Product.Id == productId).FirstOrDefault<CartItem>();
            if (c == null)
            {
                CartItem cartItem = new CartItem();
                cartItem.Quantity = quantity;
                var product = await _context.Product.FirstOrDefaultAsync(s => s.Id == productId);
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
            return true;
            //return RedirectToAction("ProductDetails","Products", new { id = (productId) });

        }

        [Authorize]
        public async Task AddProduct(int id)
        {
            this.AddToCart(id,1);
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
