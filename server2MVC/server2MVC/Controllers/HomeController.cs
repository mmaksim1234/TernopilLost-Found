using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server2MVC.Data;
using server2MVC.Migrations;
using server2MVC.Models;
using System.Diagnostics;

namespace server2MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly server2MVCContext _context;
        public HomeController(server2MVCContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> IndexAsync(Advertismnet advertismnet)
        {
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(HttpContext.Session.GetString("Id"));
            
            if (_context.Advertismnet != null)
            {
               
                    
                

                
                var activeAdvertisements = await _context.Advertismnet
                    .Where(a => a.isActive)
                    .ToListAsync();
                return View(activeAdvertisements);
            }
            else
            {
                return Problem("Entity set 'server2MVCContext.Advertismnet'  is null.");
            }

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