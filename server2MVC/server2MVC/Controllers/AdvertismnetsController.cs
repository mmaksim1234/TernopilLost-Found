    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Linq;
    using System.Threading.Tasks;
    using Grpc.Core;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using server2MVC.Data;
    using server2MVC.Migrations;
    using server2MVC.Models;

    namespace server2MVC.Controllers
    {
        public class AdvertismnetsController : Controller
        {
            private readonly server2MVCContext _context;

            public AdvertismnetsController(server2MVCContext context)
            {
                _context = context;
            }



        public async Task<IActionResult> Advertiements(string category)
        {
            ViewBag.IsLoggedIn = !string.IsNullOrEmpty(HttpContext.Session.GetString("Id"));
            if (_context.Advertismnet != null)
            {
                var advertisementsQuery = _context.Advertismnet.AsQueryable();

                if (!string.IsNullOrEmpty(category))
                {
                    advertisementsQuery = advertisementsQuery.Where(a => a.category == category);
                }

                var advertisements = await advertisementsQuery
                    .Where(a => a.isActive)
                    .ToListAsync();

                ViewBag.Categories = new List<string> { "Біжутерія", "Одяг", "Електроніка", "Документи" };
                ViewBag.CurrentCategory = category;
                return View(advertisements);
            }
            else
            {
                return Problem("Entity set 'server2MVCContext.Advertismnet' is null.");
            }
        }





        // GET: Advertismnets
        public async Task<IActionResult> Index()
            {
                  return _context.Advertismnet != null ? 
                              View(await _context.Advertismnet.ToListAsync()) :
                              Problem("Entity set 'server2MVCContext.Advertismnet'  is null.");
            }

            // GET: Advertismnets/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null || _context.Advertismnet == null)
                {
                    return NotFound();
                }

                var advertismnet = await _context.Advertismnet
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (advertismnet == null)
                {
                    return NotFound();
                }

                return View(advertismnet);
            }

            // GET: Advertismnets/Create
            public IActionResult Create()
            {
                var userId = HttpContext.Session.GetString("Id");
                if (userId == null)
                {
                    return RedirectToAction("Login","Users");
                }
                return View();
            }
            public async Task<IActionResult> MyAdvertisements()
            {
                var userId = HttpContext.Session.GetString("Id");
                if (userId == null)
                {
                    return RedirectToAction("Login", "Users");
                }

            var user = await _context.User.FindAsync(int.Parse(userId));
            var userAdvertisements = await _context.Advertismnet
                .Where(a => a.CreateUserId == user.ID)
                .ToListAsync();

            var viewModel = new UserDashboardViewModel
            {
                User = user,
                Advertisements = userAdvertisements
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Advertismnet advertismnet, IFormFile ImageFile, User user, CategoryClassifier categoryClassifier)

        {
            var userId = HttpContext.Session.GetString("Id");
            if (userId == null)
            {
                Console.WriteLine("User ID is null in session.");
            }
            else
            {
                Console.WriteLine($"User ID from session: {userId}");
            }

            if (userId != null)
            {
                advertismnet.CreateUserId = int.Parse(userId);
            }
            else
            {
                advertismnet.CreateUserId = -1;
            }
            if (ImageFile != null) // Add null check here
            {
                string fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                string extension = Path.GetExtension(ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                advertismnet.ImagePath = "~/Image/" + fileName;
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Image", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }
            }
            var titleWords = advertismnet.Title.Split(new[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            var descriptionWords = advertismnet.Description.Split(new[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            advertismnet.category = CategoryClassifier.Category(titleWords, descriptionWords);

            Random rnd = new Random();
            int randomId = rnd.Next(100000, 999999); // Мінімальне та максимальне значення для рандомного ідентифікатора
            advertismnet.UnicalId = randomId.ToString();


            _context.Add(advertismnet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Advertiements));


            return View(advertismnet);
        
    }











        // GET: Advertismnets/Edit/5
        // GET: Advertismnets/Edit/5
        // GET: Advertismnets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Advertismnet == null)
            {
                return NotFound();
            }

            var advertismnet = await _context.Advertismnet.FindAsync(id);
            if (advertismnet == null)
            {
                return NotFound();
            }

            return View(advertismnet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, bool isActive)
        {
            var advertismnet = await _context.Advertismnet.FindAsync(id);
            if (advertismnet == null)
            {
                return NotFound();
            }

            advertismnet.isActive = isActive;
            _context.Update(advertismnet);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool AdvertismnetExists(int id)
        {
            return (_context.Advertismnet?.Any(e => e.ID == id)).GetValueOrDefault();
        }















        // GET: Advertismnets/Delete/5
        public async Task<IActionResult> Delete(int? id)
            {
                var userId = HttpContext.Session.GetString("Id");
                if (userId == null)
                {
                    return RedirectToAction("Create");
                }
                if (id == null || _context.Advertismnet == null)
                {
                    return NotFound();
                }

                var advertismnet = await _context.Advertismnet
                    .FirstOrDefaultAsync(m => m.ID == id);
                if (advertismnet == null)
                {
                    return NotFound();
                }

                return View(advertismnet);
            }

            // POST: Advertismnets/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                if (_context.Advertismnet == null)
                {
                    return Problem("Entity set 'server2MVCContext.Advertismnet'  is null.");
                }
                var advertismnet = await _context.Advertismnet.FindAsync(id);
                if (advertismnet != null)
                {
                    _context.Advertismnet.Remove(advertismnet);
                }
            
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            
        }
    }
