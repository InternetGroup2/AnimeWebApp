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

        public async Task<IActionResult> Index(AnimeModel searchModel)
        {
            IQueryable<AnimeModel> query = _context.AnimeModels;

            if (!string.IsNullOrWhiteSpace(searchModel.Title))
                query = query.Where(a => a.Title.Contains(searchModel.Title));
            
            if (!string.IsNullOrWhiteSpace(searchModel.JapaneseTitle))
                query = query.Where(a => a.JapaneseTitle.Contains(searchModel.JapaneseTitle));
            
            if (!string.IsNullOrWhiteSpace(searchModel.Description))
                query = query.Where(a => a.Description.Contains(searchModel.Description));
            
            if (!string.IsNullOrWhiteSpace(searchModel.Type))
                query = query.Where(a => a.Type == searchModel.Type);
            
            if (!string.IsNullOrWhiteSpace(searchModel.Studios))
                query = query.Where(a => a.Studios.Contains(searchModel.Studios));
            
            if (searchModel.DateAired.HasValue)
                query = query.Where(a => a.DateAired >= searchModel.DateAired);
            
            if (!string.IsNullOrWhiteSpace(searchModel.Status))
                query = query.Where(a => a.Status == searchModel.Status);
            
            if (!string.IsNullOrWhiteSpace(searchModel.Genre))
                query = query.Where(a => a.Genre.Contains(searchModel.Genre));
            
            if (searchModel.Price.HasValue)
                query = query.Where(a => a.Price >= searchModel.Price);
            
            if (searchModel.Rating.HasValue)
                query = query.Where(a => a.Rating >= searchModel.Rating);
            
            if (!string.IsNullOrWhiteSpace(searchModel.Duration))
                query = query.Where(a => a.Duration == searchModel.Duration);
            
            if (!string.IsNullOrWhiteSpace(searchModel.Quality))
                query = query.Where(a => a.Quality == searchModel.Quality);
            
            if (searchModel.Views.HasValue)
                query = query.Where(a => a.Views >= searchModel.Views);
            
            if (searchModel.Votes.HasValue)
                query = query.Where(a => a.Votes >= searchModel.Votes);

            return View(await query.ToListAsync());
        }


        // GET: Anime
/*        public async Task<IActionResult> Index()
        {
            return View(await _context.AnimeModels.ToListAsync());
        }
*/
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
        public async Task<IActionResult> Create([Bind("Id,Title,JapaneseTitle,Description,Type,Studios,DateAired,Status,Genre,Price,Rating,Duration,Quality,Views,Votes,ImageData,ImageMimeType,ImageFile")] AnimeModel animeModel)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,JapaneseTitle,Description,Type,Studios,DateAired,Status,Genre,Price,Rating,Duration,Quality,Views,Votes,ImageData,ImageMimeType,ImageFile")] AnimeModel animeModel)
        {
            // Check if the provided id matches the model's id
            if (id != animeModel.Id)
            {
                return NotFound();
            }

            // Validate the model state
            if (ModelState.IsValid)
            {
                try
                {
                    // Retrieve the existing model from the database
                    var existingModel = await _context.AnimeModels.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

                    if (existingModel == null)
                    {
                        return NotFound();
                    }

                    // Update the model properties
                    _context.Entry(animeModel).State = EntityState.Modified;

                    // Check if an image file is uploaded
                    if (animeModel.ImageFile != null && animeModel.ImageFile.Length > 0)
                    {
                        // Convert the uploaded image file to a byte array
                        using (var memoryStream = new MemoryStream())
                        {
                            await animeModel.ImageFile.CopyToAsync(memoryStream);
                            animeModel.ImageData = memoryStream.ToArray(); // Store the image data
                            animeModel.ImageMimeType = animeModel.ImageFile.ContentType; // Store the MIME type
                        }
                    }
                    else
                    {
                        // If no new image is uploaded, retain the existing image data and mime type
                        animeModel.ImageData = existingModel.ImageData;
                        animeModel.ImageMimeType = existingModel.ImageMimeType;
                    }

                    // Update the model in the database context
                    _context.Update(animeModel);

                    // Save changes to the database
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Handle concurrency issues
                    if (!AnimeModelExists(animeModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // Redirect to the index action after successful edit
                return RedirectToAction(nameof(Index));
            }

            // Return the view with the model if the model state is invalid
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
