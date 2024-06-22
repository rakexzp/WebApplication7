using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication7.Areas.Identity.Data;
using WebApplication7.Data;
using WebApplication7.Models;

namespace WebApplication7.Controllers
{
    public class ApplicationRequestsController : Controller
    {
        private readonly WebApplication7Context _context;

        public ApplicationRequestsController(WebApplication7Context context)
        {
            _context = context;
        }

        // GET: ApplicationRequests
        public async Task<IActionResult> Index()
        {
            var webApplication7Context = _context.ApplicationRequest.Include(a => a.User);
            return View(await webApplication7Context.ToListAsync());
        }

        // GET: ApplicationRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationRequest = await _context.ApplicationRequest
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationRequest == null)
            {
                return NotFound();
            }

            return View(applicationRequest);
        }

        // GET: ApplicationRequests/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Set<WebApplicationUser>(), "Id", "Id");
            return View();
        }

        // POST: ApplicationRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,LastName,Email,PhoneNumber,CEO,DateOfBirth,Serial,Number,Type,CreationDate,Status,UserId")] ApplicationRequest applicationRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Set<WebApplicationUser>(), "Id", "Id", applicationRequest.UserId);
            return View(applicationRequest);
        }

        // GET: ApplicationRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationRequest = await _context.ApplicationRequest.FindAsync(id);
            if (applicationRequest == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Set<WebApplicationUser>(), "Id", "Id", applicationRequest.UserId);
            return View(applicationRequest);
        }

        // POST: ApplicationRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,LastName,Email,PhoneNumber,CEO,DateOfBirth,Serial,Number,Type,CreationDate,Status,UserId")] ApplicationRequest applicationRequest)
        {
            if (id != applicationRequest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationRequestExists(applicationRequest.Id))
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
            ViewData["UserId"] = new SelectList(_context.Set<WebApplicationUser>(), "Id", "Id", applicationRequest.UserId);
            return View(applicationRequest);
        }

        // GET: ApplicationRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationRequest = await _context.ApplicationRequest
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicationRequest == null)
            {
                return NotFound();
            }

            return View(applicationRequest);
        }

        // POST: ApplicationRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicationRequest = await _context.ApplicationRequest.FindAsync(id);
            if (applicationRequest != null)
            {
                _context.ApplicationRequest.Remove(applicationRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationRequestExists(int id)
        {
            return _context.ApplicationRequest.Any(e => e.Id == id);
        }
    }
}
