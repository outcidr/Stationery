using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stationery.Data;
using Stationery.Models;

namespace Stationery.Controllers
{
    public class RegularUsersController : Controller
    {
        private readonly StationeryContext _context;

        public RegularUsersController(StationeryContext context)
        {
            _context = context;
        }

        // GET: RegularUsers
        public async Task<IActionResult> Index()
        {
            return View(await _context.RegularUser.ToListAsync());
        }

        // GET: RegularUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regularUser = await _context.RegularUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (regularUser == null)
            {
                return NotFound();
            }

            return View(regularUser);
        }

        // GET: RegularUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RegularUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DateOfBirth,ProfilePictureUrl,Id,Username,Email,PasswordHash,CreatedAt,LastLogin")] RegularUser regularUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(regularUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(regularUser);
        }

        // GET: RegularUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regularUser = await _context.RegularUser.FindAsync(id);
            if (regularUser == null)
            {
                return NotFound();
            }
            return View(regularUser);
        }

        // POST: RegularUsers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DateOfBirth,ProfilePictureUrl,Id,Username,Email,PasswordHash,CreatedAt,LastLogin")] RegularUser regularUser)
        {
            if (id != regularUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(regularUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegularUserExists(regularUser.Id))
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
            return View(regularUser);
        }

        // GET: RegularUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var regularUser = await _context.RegularUser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (regularUser == null)
            {
                return NotFound();
            }

            return View(regularUser);
        }

        // POST: RegularUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var regularUser = await _context.RegularUser.FindAsync(id);
            if (regularUser != null)
            {
                _context.RegularUser.Remove(regularUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegularUserExists(int id)
        {
            return _context.RegularUser.Any(e => e.Id == id);
        }
    }
}
