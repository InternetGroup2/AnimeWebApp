using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnimeWebApp.Data;
using AnimeWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace AnimeWebApp.Controllers
{
    [Authorize]
    public class DiscussionController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<DiscussionController> _logger;

        public DiscussionController(AppDbContext context, UserManager<IdentityUser> userManager, ILogger<DiscussionController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        private string GetUserId() => _userManager.GetUserId(User);

        public async Task<IActionResult> Index()
        {
            return View(await _context.DiscussionModel.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserName,Topic,Content")] DiscussionModel discussionModel)
        {
            var userId = GetUserId();
            _logger.LogInformation("Attempting to create a post with UserId: {UserId}", userId);

            if (string.IsNullOrEmpty(userId))
            {
                _logger.LogWarning("UserId is null or empty. Attempt to retrieve failed.");
                ModelState.AddModelError("", "Cannot retrieve user ID.");
                return View(discussionModel);
            }


            discussionModel.UserId = userId;
            discussionModel.PostTime = DateTime.Now;
            _context.Add(discussionModel);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Post created successfully with UserId: {UserId}", userId);
            return RedirectToAction(nameof(Index));

             if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid. Errors: {ModelStateErrors}", ModelState);
                return View(discussionModel);
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussionModel = await _context.DiscussionModel.FindAsync(id);
            if (discussionModel == null)
            {
                return NotFound("Post does not exist.");
            }
            if (discussionModel.UserId != GetUserId())
            {
                return Unauthorized("Unauthorized access.");
            }

            return View(discussionModel);
        }
            

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Topic,Content")] DiscussionModel discussionModel)
        {
            if (id != discussionModel.Id)
            {
                return NotFound();
            }

            try
            {
                discussionModel.UserId = GetUserId();
                _context.Update(discussionModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!DiscussionModelExists(discussionModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex, "Failed to update the discussion post.");
                    ModelState.AddModelError("", "Failed to update the post due to a concurrency issue.");
                    return View(discussionModel);
                }
            }
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discussionModel = await _context.DiscussionModel.FirstOrDefaultAsync(m => m.Id == id);
            if (discussionModel == null || discussionModel.UserId != GetUserId())
            {
                return NotFound("Unauthorized access or post does not exist.");
            }

            return View(discussionModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discussionModel = await _context.DiscussionModel.FindAsync(id);
            if (discussionModel != null && discussionModel.UserId == GetUserId())
            {
                _context.DiscussionModel.Remove(discussionModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return NotFound("Unauthorized access or post does not exist.");
        }

        private bool DiscussionModelExists(int id)
        {
            return _context.DiscussionModel.Any(e => e.Id == id);
        }
    }
}
