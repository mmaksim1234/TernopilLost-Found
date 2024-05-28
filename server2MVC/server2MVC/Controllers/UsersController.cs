﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using server2MVC.Data;
using server2MVC.Models;
using Microsoft.AspNetCore.Identity;
using System.Drawing.Text;

using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration.UserSecrets;

namespace server2MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly server2MVCContext _context;

        public UsersController(server2MVCContext context)
        {
            _context = context;
        }
        /*public IActionResult DashBoard(User user)
        {
            var userId = HttpContext.Session.GetString("Id");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }
            return View(user);
        }*/
        
        public IActionResult DashBoard()
        {
            var userId = HttpContext.Session.GetString("Id");
            if (userId == null)
            {
                return RedirectToAction("Login");
            }

            var user = _context.User.Find(int.Parse(userId));
            if (user == null)
            {
                return NotFound();
            }

            var userAdvertisements = _context.Advertismnet
                .Where(a => a.CreateUserId == int.Parse(userId))
                .ToList();

            var viewModel = new UserDashboardViewModel
            {
                User = user,
                Advertisements = userAdvertisements
            };

            return View(viewModel);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Models.Login model)
        {
            var user = _context.User.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
            if (user != null)
            {
                
                HttpContext.Session.SetString("Id", user.ID.ToString());
                return RedirectToAction("DashBoard", user);
            }
            else
            {
                
                // Помилка: невірний електронний лист або пароль
                ModelState.AddModelError(string.Empty, "Невірний електронний лист або пароль");
                return View();

            }
        }
            // GET: Users
            public async Task<IActionResult> Index()
        {
            return View();

        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.Password != user.repeatPassword)
                {
                    ModelState.AddModelError("RepeatPassword", "Паролі не співпадають");
                    return View(user);
                }
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Login)) ;
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,Email,Password")] User user)
        {
            if (id != user.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.ID))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'server2MVCContext.User'  is null.");
            }
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.User?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
 