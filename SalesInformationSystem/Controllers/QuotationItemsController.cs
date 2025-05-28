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
    public class QuotationItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuotationItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: QuotationItems

        public async Task<IActionResult> ListQuotationItems(int id)
        {
            var getQuoationItems = _context.QuotationItem.Where(qi => qi.QuotationId == id).Include(qi=>qi.Product).ToList();
            return View(getQuoationItems);
        }
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.QuotationItem.Include(q => q.Product).Include(q => q.Quotation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: QuotationItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotationItem = await _context.QuotationItem
                .Include(q => q.Product)
                .Include(q => q.Quotation)
                .FirstOrDefaultAsync(m => m.QuotationItemId == id);
            if (quotationItem == null)
            {
                return NotFound();
            }

            return View(quotationItem);
        }

        // GET: QuotationItems/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId");
            ViewData["QuotationId"] = new SelectList(_context.Quotation, "QuotationId", "QuotationId");
            return View();
        }

        // POST: QuotationItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuotationItemId,Quantity,Price,Discount,QuotationId,ProductId")] QuotationItem quotationItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quotationItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId", quotationItem.ProductId);
            ViewData["QuotationId"] = new SelectList(_context.Quotation, "QuotationId", "QuotationId", quotationItem.QuotationId);
            return View(quotationItem);
        }

        // GET: QuotationItems/Edit/5
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotationItem = await _context.QuotationItem.FindAsync(id);
            if (quotationItem == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId", quotationItem.ProductId);
            ViewData["QuotationId"] = new SelectList(_context.Quotation, "QuotationId", "QuotationId", quotationItem.QuotationId);
            return View(quotationItem);
        }

        // POST: QuotationItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuotationItemId,Quantity,Price,Discount,QuotationId,ProductId")] QuotationItem quotationItem)
        {
            if (id != quotationItem.QuotationItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quotationItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuotationItemExists(quotationItem.QuotationItemId))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId", quotationItem.ProductId);
            ViewData["QuotationId"] = new SelectList(_context.Quotation, "QuotationId", "QuotationId", quotationItem.QuotationId);
            return View(quotationItem);
        }

        // GET: QuotationItems/Delete/5
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quotationItem = await _context.QuotationItem
                .Include(q => q.Product)
                .Include(q => q.Quotation)
                .FirstOrDefaultAsync(m => m.QuotationItemId == id);
            if (quotationItem == null)
            {
                return NotFound();
            }

            return View(quotationItem);
        }

        // POST: QuotationItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quotationItem = await _context.QuotationItem.FindAsync(id);
            if (quotationItem != null)
            {
                _context.QuotationItem.Remove(quotationItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuotationItemExists(int id)
        {
            return _context.QuotationItem.Any(e => e.QuotationItemId == id);
        }
    }
}
