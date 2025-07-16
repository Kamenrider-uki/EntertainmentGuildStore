using Microsoft.AspNetCore.Mvc;
using EntertainmentGuildStore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EntertainmentGuildStore.Controllers
{
    public class ItemController : Controller
    {
        private readonly EntertainmentGuildDbContext _context;

        public ItemController(EntertainmentGuildDbContext context)
        {
            _context = context;
        }

        private bool IsAdmin()
        {
            var userEmail = HttpContext.Session.GetString("User");
            return userEmail?.ToLower() == "admin@1";
        }

        private SelectList GetCategoryList()
        {
            return new SelectList(new List<string>
    {
        "Games",
        "Anime Figures",
        "Peripherals",
        "Collectibles",
        "Console Hardware",
        "Card Games",
        "Plush Toys",
        "Merchandise",
        "Pre-Owned Deals",
        "New Arrivals",
        "Top Rated"
    });
        }


        public IActionResult Index()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var items = _context.Items.ToList();
            return View("Admin/Index", items);
        }

        public IActionResult Details(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var item = _context.Items.Find(id);
            if (item == null) return NotFound();

            return View("Admin/Details", item);
        }

        public IActionResult Create()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            ViewBag.Categories = GetCategoryList();
            return View("Admin/Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Item item, IFormFile MainImageFile, IFormFile? SideImageFile)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            if (!ModelState.IsValid || MainImageFile == null)
            {
                ModelState.AddModelError("MainImage", "Main image is required.");
                ViewBag.Categories = GetCategoryList();
                return View("Admin/Create", item);
            }

            using (var ms = new MemoryStream())
            {
                await MainImageFile.CopyToAsync(ms);
                item.MainImage = ms.ToArray();
            }

            if (SideImageFile != null)
            {
                using var ms2 = new MemoryStream();
                await SideImageFile.CopyToAsync(ms2);
                item.SideImage = ms2.ToArray();
            }

            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var item = _context.Items.Find(id);
            if (item == null) return NotFound();

            ViewBag.Categories = GetCategoryList();
            return View("Admin/Edit", item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Item item, IFormFile? MainImageFile, IFormFile? SideImageFile)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var existing = _context.Items.FirstOrDefault(i => i.Id == item.Id);
            if (existing == null) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = GetCategoryList();
                return View("Admin/Edit", item);
            }

            // Update main fields
            existing.Name = item.Name;
            existing.Description = item.Description;
            existing.Price = item.Price;
            existing.Category = item.Category;
            existing.Feature1 = item.Feature1;
            existing.Feature2 = item.Feature2;
            existing.Feature3 = item.Feature3;
            existing.Feature4 = item.Feature4;

            if (MainImageFile != null)
            {
                using var ms = new MemoryStream();
                await MainImageFile.CopyToAsync(ms);
                existing.MainImage = ms.ToArray();
            }

            if (SideImageFile != null)
            {
                using var ms2 = new MemoryStream();
                await SideImageFile.CopyToAsync(ms2);
                existing.SideImage = ms2.ToArray();
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");

            var item = _context.Items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                _context.Items.Remove(item);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Deactivate(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            SetItemActiveStatus(id, false);
            return RedirectToAction("Index");
        }

        public IActionResult Activate(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account");
            SetItemActiveStatus(id, true);
            return RedirectToAction("Index");
        }

        private void SetItemActiveStatus(int id, bool isActive)
        {
            var item = _context.Items.Find(id);
            if (item != null)
            {
                item.IsActive = isActive;
                _context.SaveChanges();
            }
        }
    }
}
