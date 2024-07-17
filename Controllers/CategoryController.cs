using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AnimeWebApp.Data;
using AnimeWebApp.Models;

public class CategoryController : Controller
{
    private readonly AppDbContext _context;

    public CategoryController(AppDbContext context)
    {
        _context = context;
    }

    // Action to display categories
    public IActionResult Index()
    {
        try
        {
            var categories = _context.Categories.ToList(); // Attempt to fetch categories
            ViewBag.ConnectionMessage = "Connected"; // Set a success message
        }
        catch (Exception ex)
        {
            ViewBag.ConnectionMessage = "Failed to connect: " + ex.Message; // Set a failure message
        }

        return View();
        
    }

    // Additional actions can be added here such as Create, Edit, Delete, etc.
}