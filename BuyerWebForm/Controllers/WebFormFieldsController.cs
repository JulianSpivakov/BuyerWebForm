using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BuyerWebForm.Data;
using BuyerWebForm.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BuyerWebForm.Controllers
{
    public class WebFormFieldsController : Controller
    {
        private readonly WebFormContext _context;

        public WebFormFieldsController(WebFormContext context)
        {
            _context = context;
        }

        // GET: WebFormFields
        public async Task<IActionResult> Index()
        {
            return View(await _context.WebFormFields.ToListAsync());
        }

        // GET: WebFormFields/Details/5
        public async Task<IActionResult> Details(ObjectId? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webFormFields = await _context.WebFormFields
                .FirstOrDefaultAsync(m => m.Id == id);
            if (webFormFields == null)
            {
                return NotFound();
            }

            return View(webFormFields);
        }

        // GET: WebFormFields/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WebFormFields/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,EmailAddress,PhoneNumber,Address")] WebFormFields webFormFields)
        {
            if (ModelState.IsValid)
            {
                _context.Add(webFormFields);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(webFormFields);
        }

        // GET: WebFormFields/Edit/5
        public async Task<IActionResult> Edit(ObjectId? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webFormFields = await _context.WebFormFields.FindAsync(id);
            if (webFormFields == null)
            {
                return NotFound();
            }
            return View(webFormFields);
        }

        // POST: WebFormFields/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ObjectId id, [Bind("Id,FirstName,LastName,EmailAddress,PhoneNumber,Address")] WebFormFields webFormFields)
        {
            if (id != webFormFields.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(webFormFields);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WebFormFieldsExists(webFormFields.Id))
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
            return View(webFormFields);
        }

        // GET: WebFormFields/Delete/5
        public async Task<IActionResult> Delete(ObjectId? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var webFormFields = await _context.WebFormFields
                .FirstOrDefaultAsync(m => m.Id == id);
            if (webFormFields == null)
            {
                return NotFound();
            }

            return View(webFormFields);
        }

        // POST: WebFormFields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ObjectId id)
        {
            var webFormFields = await _context.WebFormFields.FindAsync(id);
            if (webFormFields != null)
            {
                _context.WebFormFields.Remove(webFormFields);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WebFormFieldsExists(ObjectId id)
        {
            return _context.WebFormFields.Any(e => e.Id == id);
        }
    }
}
