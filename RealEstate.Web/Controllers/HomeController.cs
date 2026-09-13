using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Interfaces;

namespace RealEstate.Web.Controllers;

public class HomeController : Controller
{
    private readonly IPropertyService _propertyService;

    public HomeController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    // GET: /
    // GET: /Home
    public async Task<IActionResult> Index()
    {
        var properties = await _propertyService.GetAllAsync();
        return View(properties);
    }

    // GET: /Home/Favorites
    public async Task<IActionResult> Favorites()
    {
        var properties = await _propertyService.GetAllAsync();
        return View(properties);
    }
}
