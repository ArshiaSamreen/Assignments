using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AzureFinalAssignment.Models;
using System.Security.Policy;
using AzureFinalAssignment.Service;

namespace AzureFinalAssignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly PublishMessage _publish;        

        public HomeController(ILogger<HomeController> logger, PublishMessage publish)
        {
            _logger = logger;
            _publish = publish;            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Message()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Message([Bind("UserName,EmailAddress,PhoneNumber")] PromotionMessage message)
        {
            if (ModelState.IsValid)
            {
                string result = await _publish.SendMessagesAsync(message);
                //return RedirectToAction(nameof(PromotionMessage));    
                ViewData["response"] = result;
                return View();
            };
            return View(message);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
