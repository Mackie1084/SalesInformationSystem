using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesInformationSystem.Data;
using SalesInformationSystem.Models;

namespace SalesInformationSystem.Controllers
{
    public class QuotationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuotationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Quotations

        [Authorize(Roles ="Customer")]
        public async Task<IActionResult> MyQuotations()
        {
                   
            var username = User.Identity.Name;
            var getCustId = _context.Customer.Where(c => c.Email == username).FirstOrDefault().CustomerId;
            var getQuotations = _context.Quotation.Where(q => q.CustomerId == getCustId).ToList();
            return View(getQuotations);
        }
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Quotation.Include(q => q.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Quotations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotation = await _context.Quotation
                .Include(q => q.Customer)
                .FirstOrDefaultAsync(m => m.QuotationId == id);
            if (quotation == null)
            {
                return NotFound();
            }

            return View(quotation);
        }

        // GET: Quotations/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerId");
            return View();
        }

        // POST: Quotations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuotationId,CreatedBy,TotalAmount,Status,ExpiryDate,CustomerId")] Quotation quotation)
        {
          //  if (ModelState.IsValid)
           // {
                _context.Add(quotation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
         //   }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerId", quotation.CustomerId);
            return View(quotation);
        }

        // GET: Quotations/Edit/5
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotation = await _context.Quotation.FindAsync(id);
            if (quotation == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerId", quotation.CustomerId);
            return View(quotation);
        }

        // POST: Quotations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuotationId,CreatedBy,TotalAmount,Status,ExpiryDate,CustomerId")] Quotation quotation)
        {
            if (id != quotation.QuotationId)
            {
                return NotFound();
            }

           // if (ModelState.IsValid)
          //  {
                try
                {
                    _context.Update(quotation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuotationExists(quotation.QuotationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerId", quotation.CustomerId);
            return View(quotation);
        }

        // GET: Quotations/Delete/5
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotation = await _context.Quotation
                .Include(q => q.Customer)
                .FirstOrDefaultAsync(m => m.QuotationId == id);
            if (quotation == null)
            {
                return NotFound();
            }

            return View(quotation);
        }

        // POST: Quotations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quotation = await _context.Quotation.FindAsync(id);
            if (quotation != null)
            {
                _context.Quotation.Remove(quotation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuotationExists(int id)
        {
            return _context.Quotation.Any(e => e.QuotationId == id);
        }
    }
}
