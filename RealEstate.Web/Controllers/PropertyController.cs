using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs.Property;
using RealEstate.Application.Interfaces;

namespace RealEstate.Web.Controllers;

public class PropertyController : Controller
{
    private readonly IPropertyService _propertyService;
    private readonly IAgentService _agentService;
    private readonly IPropertyImageService _imageService;

    public PropertyController(
        IPropertyService propertyService,
        IAgentService agentService,
        IPropertyImageService imageService)
    {
        _propertyService = propertyService;
        _agentService = agentService;
        _imageService = imageService;
    }

    // GET: /Property
    public async Task<IActionResult> Index()
    {
        var properties = await _propertyService.GetAllAsync();
        return View(properties);
    }

    // GET: /Property/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var property = await _propertyService.GetByIdAsync(id);

        if (property is null)
            return NotFound();

        var images = await _imageService.GetByPropertyIdAsync(id);
        ViewBag.Images = images;

        if (property.AgentId > 0)
        {
            ViewBag.Agent = await _agentService.GetByIdAsync(property.AgentId);
        }

        return View(property);
    }

    // GET: /Property/Create
    public async Task<IActionResult> Create()
    {
        ViewBag.Agents = await _agentService.GetAllAsync();
        return View();
    }

    // POST: /Property/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreatePropertyDto dto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Agents = await _agentService.GetAllAsync();
            return View(dto);
        }

        var propertyId = await _propertyService.CreateAsync(dto);

        return RedirectToAction(nameof(Details), new { id = propertyId });
    }

    // GET: /Property/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var property = await _propertyService.GetByIdAsync(id);

        if (property is null)
            return NotFound();

        var dto = new UpdatePropertyDto
        {
            Title = property.Title,
            Description = property.Description,
            Price = property.Price,
            PropertyType = property.PropertyType,
            TransactionType = property.TransactionType,
            Area = property.Area,
            Bedrooms = property.Bedrooms,
            Bathrooms = property.Bathrooms,
            Floor = property.Floor,
            Furnished = property.Furnished,
            Address = property.Address,
            City = property.City,
            Latitude = property.Latitude,
            Longitude = property.Longitude,
            AgentId = property.AgentId
        };

        ViewBag.Agents = await _agentService.GetAllAsync();
        return View(dto);
    }

    // POST: /Property/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        UpdatePropertyDto dto)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Agents = await _agentService.GetAllAsync();
            return View(dto);
        }

        var result = await _propertyService.UpdateAsync(id, dto);

        if (!result)
            return NotFound();

        return RedirectToAction(nameof(Details), new { id });
    }

    // POST: /Property/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _propertyService.DeleteAsync(id);

        if (!result)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }
}