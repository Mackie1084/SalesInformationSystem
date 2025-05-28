using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesInformationSystem.Data;
using SalesInformationSystem.Models;

namespace SalesInformationSystem.Controllers
{
    public class OperationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OperationsController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }


        public IActionResult Test()
        {
            List<string> menuItems = new List<string> { "Dashboard", "Profile", "Settings", "Logout" };
            ViewBag.MenuItems = menuItems;
            return View();
        }

        public IActionResult Display(string StudName, int num)
        {
            List<string> names = new List<string>();
            for (int i = 1; i <= num; i++)
            {
                names.Add(StudName);
            }
            ViewBag.lstNames = names;
            ViewBag.name = StudName;
            ViewBag.n = num;
            return View();
        }

        [HttpGet]
     
        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct([Bind("ProductId,ProductName,ProductPrice,StockQuantity")] Product product)
        {

            //if (ModelState.IsValid)
            //{
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            // }
            return View(product);
      
        }
    }
}
