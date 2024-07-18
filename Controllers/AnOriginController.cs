using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AnimeWebApp.Data;
using AnimeWebApp.Models;

public class AnOriginController : Controller
{
    private readonly AppDbContext _context;

    public AnOriginController(AppDbContext context)
    {
        _context = context;
    }

    // Action to display categories
    public IActionResult Index()
    {
        try
        {
            var AnOrigin = _context.AnOrigins.ToList(); // Attempt to fetch categories
            ViewBag.ConnectionMessage = "Connected"; // Set a success message
        }
        catch (Exception ex)
        {
            ViewBag.ConnectionMessage = "Failed to connect: " + ex.Message; // Set a failure message
        }

        return View();
        
    }
    
    public IActionResult Watching()
        {
            return View();
        }

    // Additional actions can be added here such as Create, Edit, Delete, etc.
}