using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AnimeWebApp.Data;
using AnimeWebApp.Models;

namespace AnimeWebApp.Controllers
{
    public class AnimeController : Controller
    {
        private readonly AppDbContext _context;

        public AnimeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Anime
        public async Task<IActionResult> Index()
        {
            return View(await _context.AnimeModels.ToListAsync());
        }

        // GET: Anime/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animeModel = await _context.AnimeModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animeModel == null)
            {
                return NotFound();
            }

            return View(animeModel);
        }

        // GET: Anime/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Anime/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,JapaneseTitle,Description,Type,Studios,DateAired,Status,Genre,Score,Rating,Duration,Quality,Views,Votes,ImageData,ImageMimeType,ImageFile")] AnimeModel animeModel)
        {
            if (ModelState.IsValid)
            {
                if (animeModel.ImageFile != null && animeModel.ImageFile.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await animeModel.ImageFile.CopyToAsync(memoryStream);
                        animeModel.ImageData = memoryStream.ToArray(); // Store the image data
                        animeModel.ImageMimeType = animeModel.ImageFile.ContentType; // Store the MIME type
                    }
                }

                _context.Add(animeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(animeModel);
        }


        // GET: Anime/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animeModel = await _context.AnimeModels.FindAsync(id);
            if (animeModel == null)
            {
                return NotFound();
            }
            return View(animeModel);
        }

        // POST: Anime/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,JapaneseTitle,Description,Type,Studios,DateAired,Status,Genre,Score,Rating,Duration,Quality,Views,Votes,ImageData,ImageMimeType")] AnimeModel animeModel)
        {
            if (id != animeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimeModelExists(animeModel.Id))
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
            return View(animeModel);
        }

        // GET: Anime/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var animeModel = await _context.AnimeModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animeModel == null)
            {
                return NotFound();
            }

            return View(animeModel);
        }

        // POST: Anime/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animeModel = await _context.AnimeModels.FindAsync(id);
            if (animeModel != null)
            {
                _context.AnimeModels.Remove(animeModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimeModelExists(int id)
        {
            return _context.AnimeModels.Any(e => e.Id == id);
        }
    }
}
