using Microsoft.AspNetCore.Mvc;
using EntertainmentGuildStore.Models;
using Microsoft.AspNetCore.Http; // For session

namespace EntertainmentGuildStore.Controllers
{
    public class ItemController : Controller
    {
        private static List<Item> items = new List<Item>
        {
            new Item { Id = 1, Name = "Game controller", Description = "Wireless Bluetooth controller", Price = 299.99M },
            new Item { Id = 2, Name = "Earphones", Description = "Bluetooth Wireless Noise Cancelling Earphones", Price = 899.00M },
            new Item { Id = 3, Name = "Keyboard", Description = "Noiseless mechanical keyboard", Price = 699.50M }
        };

        public IActionResult Index()
        {
            // reorient
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                return RedirectToAction("Register", "Account");
            }

            return View(items);
        }

        public IActionResult Details(int id)
        {
            // Login check
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                return RedirectToAction("Register", "Account");
            }

            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }
    }
}
