using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LCPStore.Data;
using LCPStore.Models;
using System.Net;
using System.Net.Mail;

namespace LCPStore.Controllers
{
    
    public class ContactsController : Controller
    {
        private readonly LCPStoreContext _context;

        public ContactsController(LCPStoreContext context)
        {
            _context = context;
        }

        // GET: Contacts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Contact.ToListAsync());
        }

        [ValidateAntiForgeryToken]
        public void SendEmail(string Email, string Name)
        {
            var fromAddress = new MailAddress("LCPStore4@gmail.com", "LCP Store");
            var toAddress = new MailAddress(Email, Name);
            const string fromPassword = "LCP12345678";
            const string subject = "Thank you for contacting LCP";
            const string body = "Thank you for contacting LCP support";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

        // GET: Contacts/Create
        public IActionResult Contact()
        {
            return View();
        }

        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<bool> Contact([Bind("Id,Name,Email,Subject,Body")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contact);
                await _context.SaveChangesAsync();
                SendEmail(contact.Email, contact.Name);
                //return redirecttoaction(nameof(index));
                return true;
            }
            return false;
            //return View(contact);
        }

        private bool ContactExists(int id)
        {
            return _context.Contact.Any(e => e.Id == id);
            
        }
    }
}
