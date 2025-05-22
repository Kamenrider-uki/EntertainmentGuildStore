using Microsoft.AspNetCore.Mvc;
using EntertainmentGuildStore.Models;

namespace EntertainmentGuildStore.Controllers
{
    public class ItemController : Controller
    {
        private static List<Item> items = new List<Item>
        {
            new Item { Id = 1, Name = "Game controller", Description = "Wireless Bluetooth controller", Price = 299.99M },
            new Item { Id = 2, Name = "Earphones", Description = "Bluetooth Wireless Noise Cancelling Headphones", Price = 899.00M },
            new Item { Id = 3, Name = "Keyboard", Description = "Noiseless mechanical keyboard", Price = 699.5M }
        };

        public IActionResult Index()
        {
            return View(items);
        }

        public IActionResult Details(int id)
        {
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }
    }
}
