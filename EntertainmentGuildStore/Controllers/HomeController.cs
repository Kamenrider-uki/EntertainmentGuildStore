using EntertainmentGuildStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EntertainmentGuildStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var items = new List<Item>
            {
                new Item { Id = 1, Name = "Game Controller", Description = "Wireless Bluetooth controller", Price = 299.99M },
                new Item { Id = 2, Name = "Earphones", Description = "Noise Cancelling Earphones", Price = 899.00M },
                new Item { Id = 3, Name = "Keyboard", Description = "Mechanical keyboard", Price = 699.50M }
            };

            return View(items);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
