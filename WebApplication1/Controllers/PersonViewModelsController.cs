using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class PersonViewModelsController : Controller
    {
        private readonly WebApplication1Context _context;

        public PersonViewModelsController(WebApplication1Context context)
        {
            _context = context;
        }

        // GET: PersonViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.PersonViewModel.ToListAsync());
        }

        // GET: PersonViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personViewModel = await _context.PersonViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personViewModel == null)
            {
                return NotFound();
            }

            return View(personViewModel);
        }

        // GET: PersonViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersonViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PersonViewModel personViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personViewModel);
        }

        // GET: PersonViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personViewModel = await _context.PersonViewModel.FindAsync(id);
            if (personViewModel == null)
            {
                return NotFound();
            }
            return View(personViewModel);
        }

        // POST: PersonViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PersonViewModel personViewModel)
        {
            if (id != personViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonViewModelExists(personViewModel.Id))
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
            return View(personViewModel);
        }

        // GET: PersonViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personViewModel = await _context.PersonViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personViewModel == null)
            {
                return NotFound();
            }

            return View(personViewModel);
        }

        // POST: PersonViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personViewModel = await _context.PersonViewModel.FindAsync(id);
            _context.PersonViewModel.Remove(personViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonViewModelExists(int id)
        {
            return _context.PersonViewModel.Any(e => e.Id == id);
        }
    }
}
