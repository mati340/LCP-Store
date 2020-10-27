using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LCPStore.Data;
using LCPStore.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace LCPStore.Controllers
{
    public class AccountsController : Controller
    {
        private readonly LCPStoreContext _context;

        public AccountsController(LCPStoreContext context)
        {
            _context = context;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Username,Password,Gender,Name,BirthDate,Registered,Role")] Account account, string re_pass, string agree_term)
        {
            if(agree_term != "true")
            {
                TempData["TermsError"] = "You must agree to the terms";
                return View(account);
            }
            var user = await _context.Account.FirstOrDefaultAsync(u => account.Username == u.Username);
            if (user != null)
            {
                ModelState.AddModelError("Username", "This Email is already exist");
                return View();
            }
            if(account.Password != re_pass)
            {
                TempData["Error"] = "The password doesn't match";
                account.Password = null;
                return View(account);
            }
            if (ModelState.IsValid)
            {
                account.Registered = DateTime.Now;
                _context.Add(account);
                await _context.SaveChangesAsync();
                SignIn(account);
                return RedirectToAction(nameof(Index), "Home");
            }
            return View(account);
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string Username, string Password, string Remember_me)
        {
            var account = await _context.Account.FirstOrDefaultAsync(u => Username == u.Username);
            if ( account == null)
            {
                ModelState.AddModelError("Username", "This Email doesn't exist");
                return View();
            }
            account = await _context.Account.FirstOrDefaultAsync(u => (Username == u.Username) && (Password == u.Password));
            if (account == null)
            {
                ModelState.AddModelError("Password", "Wrong Password");
                return View();
            }
            SignIn(account, Remember_me);
            return RedirectToAction(nameof(Index), "Home");
        }

        private async void SignIn(Account user, string Remember_me = "false")
        {
            var claims = new List<Claim>
            {

                new Claim(ClaimTypes.Email, user.Username),
                new Claim("Email", user.Username),
                new Claim("Name", user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties();
            if (Remember_me == "false")
            {
                authProperties.ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10);
            }
            else
            {
                authProperties.ExpiresUtc = DateTimeOffset.UtcNow.AddDays(3);
            }

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Index), "Home");
        }
        // GET: Accounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Account.ToListAsync());
        }

        // GET: Accounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Accounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password,Gender,Name,BirthDate,Registered,Role")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,Gender,Name,BirthDate,Registered,Role")] Account account)
        {
            if (id != account.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.Id))
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
            return View(account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Account
                .FirstOrDefaultAsync(m => m.Id == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var account = await _context.Account.FindAsync(id);
            _context.Account.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(int id)
        {
            return _context.Account.Any(e => e.Id == id);
        }
    }
}
