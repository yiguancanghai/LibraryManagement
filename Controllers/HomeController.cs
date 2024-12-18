using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;

namespace LibraryManagement.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    // Custom error page that takes an error message as a parameter
    public IActionResult Error(string message = null)
    {
        // Log the error with details
        var requestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        _logger.LogError($"An error occurred. RequestId: {requestId}, Message: {message ?? "No specific error message"}");

        // Display a user-friendly error view with RequestId and custom error message
        var errorViewModel = new ErrorViewModel
        {
            RequestId = requestId,
            Message = message
        };

        return View(errorViewModel);
    }
}