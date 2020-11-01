using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LCPStore.Data;
using LCPStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LCPStore.Controllers
{
    public class AdminPanelsController : Controller
    {
        private readonly LCPStoreContext _context;

        public AdminPanelsController(LCPStoreContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Tables()
        {
            AdminPanel ap = new AdminPanel
            {
                Accounts = _context.Account.ToList(),
                //ap.Orders = _context.Order.ToList();
                Products = _context.Product.ToList(),
                Categories = _context.Category.ToList(),
                Contacts = _context.Contact.ToList()

            };

            return View(ap);
        }

        // Search-Auto Complete
        public async Task<IActionResult> Search(string term)
        {
            AdminPanel ap = new AdminPanel
            {
                Accounts = _context.Account.Where(s => (s.Name.Contains(term))
                                                    || (s.Username.Contains(term)))
                                                    .ToList(),
                //ap.Orders = _context.Order.ToList();
                Products = _context.Product.Where(s => s.Name.Contains(term)
                                                    || (s.Description.Contains(term))
                                                    || (s.Price.ToString().Equals(term)))
                                                    .ToList(),
                Categories = _context.Category.Where(s => s.Name.Contains(term)).ToList(),
                Contacts = _context.Contact.Where(s => s.Subject.Contains(term)
                                                    || s.Body.Contains(term)
                                                    || s.Email.Contains(term))
                                                    .ToList()

            };

            return View(ap);
        }

        //public ActionResult Charts()
        //{
        //    var result = (from o in _context.OrderDetail
        //                  group o by o.Product.Name into o
        //                  orderby o.Count() descending
        //                  select new { o.Key, Total = o.Count() })
        //     .ToDictionary(x => x.Key, x => x.Total);
        //    ViewBag.OrderCount = result;
        //    var DateResult = _context.Order.GroupBy(x => x.OrderPlaced.Date, x => x.Id)
        //                .Select(x => new { Date = x.Key.ToShortDateString(), Count = x.Count() }).ToList();
        //    ViewBag.DateResult = DateResult;
        //    return View("Index");
        //}

        // GET: Accounts/Details/5
        public async Task<IActionResult> AccountDetails(int? id)
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

            return View("~/Views/AdminPanels/Accounts/Details.cshtml", account);
        }

        // GET: Accounts/Create
        public IActionResult AccountCreate()
        {
            return View("~/Views/AdminPanels/Accounts/Create.cshtml");
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccountCreate([Bind("Id,Username,Password,Gender,Name,BirthDate,Registered,Role")] Account account)
        {
            if (ModelState.IsValid)
            {
                _context.Add(account);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Tables));
            }
            return View("~/Views/AdminPanels/Accounts/Create.cshtml", account);
        }

        // GET: Accounts/Edit/5
        public async Task<IActionResult> AccountEdit(int? id)
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
            return View("~/Views/AdminPanels/Accounts/Edit.cshtml", account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccountEdit(int id, [Bind("Id,Username,Password,Gender,Name,BirthDate,Registered,Role")] Account account)
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
                return RedirectToAction(nameof(Tables));
            }
            return View("~/Views/AdminPanels/Accounts/Edit.cshtml", account);
        }

        // GET: Accounts/Delete/5
        public async Task<IActionResult> AccountDelete(int? id)
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

            return View("~/Views/AdminPanels/Accounts/Delete.cshtml", account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("AccountDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AccountDeleteConfirmed(int id)
        {
            var account = await _context.Account.FindAsync(id);
            _context.Account.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Tables));
        }
        private bool AccountExists(int id)
        {
            return _context.Account.Any(e => e.Id == id);
        }


        // GET: Products/Details/5
        public async Task<IActionResult> ProductDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View("~/Views/AdminPanels/Products/Details.cshtml", product);
        }

        // GET: Products/Create
        public IActionResult ProductCreate()
        {
            ViewBag.Categories = new SelectList(_context.Category.ToList(), "Id", "Name");
            return View("~/Views/AdminPanels/Products/Create.cshtml");
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate([Bind("Id,Name,Description,Price,ImageFile,Created")] Product product, string categoryId)
        {
            if (ModelState.IsValid)
            {
                product.Created = DateTime.Now;

                using (MemoryStream ms = new MemoryStream())
                {
                    product.ImageFile.CopyTo(ms);
                    product.Image = ms.ToArray();
                }

                var category = await _context.Category.FirstAsync(c => c.Id.ToString() == categoryId);

                product.Category = category;

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Tables));
            }
            return View("~/Views/AdminPanels/Products/Create.cshtml", product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> ProductEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View("~/Views/AdminPanels/Products/Edit.cshtml", product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductEdit(int id, [Bind("Id,Name,Description,Price,Created")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Tables));
            }
            return View("~/Views/AdminPanels/Products/Edit.cshtml", product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> ProductDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View("~/Views/AdminPanels/Products/Delete.cshtml", product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("ProductDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Tables));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> ContactDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View("~/Views/AdminPanels/Contacts/Details.cshtml", contact);
        }

        // GET: Contacts/Create
        public IActionResult ContactCreate()
        {
            return View("~/Views/AdminPanels/Contacts/Create.cshtml");
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactCreate([Bind("Id,Name,Email,Subject,Body")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                RedirectToAction("SendEmail", "Contacts", new { contact.Email, contact.Name });
                return RedirectToAction(nameof(Tables));
            }
            return View("~/Views/AdminPanels/Contacts/Create.cshtml", contact);
        }

        // GET: Contacts/Edit/5
        public async Task<IActionResult> ContactEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View("~/Views/AdminPanels/Contacts/Edit.cshtml", contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactEdit(int id, [Bind("Id,Name,Subject,Body")] Contact contact)
        {
            if (id != contact.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contact);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contact.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Tables));
            }
            return View("~/Views/AdminPanels/Contacts/Edit.cshtml", contact);
        }

        // GET: Contacts/Delete/5
        public async Task<IActionResult> ContactDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contact
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View("~/Views/AdminPanels/Contacts/Delete.cshtml", contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("ContactDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactDeleteConfirmed(int id)
        {
            var contact = await _context.Contact.FindAsync(id);
            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Tables));
        }

        private bool ContactExists(int id)
        {
            return _context.Contact.Any(e => e.Id == id);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> CategoryDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View("~/Views/AdminPanels/Categories/Details.cshtml", category);
        }

        // GET: Categories/Create
        public IActionResult CategoryCreate()
        {
            return View("~/Views/AdminPanels/Categories/Create.cshtml");
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryCreate([Bind("Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Tables));
            }
            return View("~/Views/AdminPanels/Categories/Create.cshtml", category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> CategoryEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View("~/Views/AdminPanels/Categories/Edit.cshtml", category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryEdit(int id, [Bind("Id,Name")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Tables));
            }
            return View("~/Views/AdminPanels/Categories/Edit.cshtml", category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> CategoryDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View("~/Views/AdminPanels/Categories/Delete.cshtml", category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("CategoryDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CategoryDeleteConfirmed(int id)
        {
            var category = await _context.Category.FindAsync(id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Tables));
        }


        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
    }
}
