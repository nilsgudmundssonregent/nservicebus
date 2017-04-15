using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Messages;
using Microsoft.AspNetCore.Mvc;
using NServiceBus;

namespace WebShop.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Buy()
        {
            Thread.Sleep(10*1000);
            var command = new PlaceOrder
            {
                OrderId = Guid.NewGuid().ToString()
            };
            await Startup.endpointInstance.Send(command).ConfigureAwait(false);
            return View();
        }
    }
}
