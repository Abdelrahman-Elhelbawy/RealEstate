using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs.PropertyImage;
using RealEstate.Application.Interfaces;

namespace RealEstate.Web.Controllers;

public class PropertyImageController : Controller
{
    private readonly IPropertyImageService _service;
    private readonly IPropertyService _propertyService;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public PropertyImageController(
        IPropertyImageService service,
        IPropertyService propertyService,
        IWebHostEnvironment webHostEnvironment)
    {
        _service = service;
        _propertyService = propertyService;
        _webHostEnvironment = webHostEnvironment;
    }

    // GET: /PropertyImage
    public async Task<IActionResult> Index()
    {
        var images = await _service.GetAllAsync();
        return View(images);
    }

    // GET: /PropertyImage/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var image = await _service.GetByIdAsync(id);

        if (image is null)
            return NotFound();

        return View(image);
    }

    // GET: /PropertyImage/Create
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Properties = await _propertyService.GetAllAsync();
        return View();
    }

    // POST: /PropertyImage/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CreatePropertyImageDto dto,
        IFormFile? imageFile)
    {
        try
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "properties");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                dto.ImageUrl = "/images/properties/" + fileName;
            }

            var id = await _service.CreateAsync(dto);

            return RedirectToAction(
                nameof(Details),
                new { id });
        }
        catch (ValidationException ex)
        {
            foreach (var error in ex.Errors)
            {
                ModelState.AddModelError(
                    error.PropertyName,
                    error.ErrorMessage);
            }

            ViewBag.Properties = await _propertyService.GetAllAsync();
            return View(dto);
        }
        catch (KeyNotFoundException ex)
        {
            ModelState.AddModelError(
                nameof(dto.PropertyId),
                ex.Message);

            ViewBag.Properties = await _propertyService.GetAllAsync();
            return View(dto);
        }
    }

    // GET: /PropertyImage/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var image = await _service.GetByIdAsync(id);

        if (image is null)
            return NotFound();

        var dto = new UpdatePropertyImageDto
        {
            ImageUrl = image.ImageUrl,
            IsPrimary = image.IsPrimary,
            DisplayOrder = image.DisplayOrder,
            PropertyId = image.PropertyId
        };

        ViewBag.Properties = await _propertyService.GetAllAsync();
        return View(dto);
    }

    // POST: /PropertyImage/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        UpdatePropertyImageDto dto,
        IFormFile? imageFile)
    {
        try
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "properties");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                dto.ImageUrl = "/images/properties/" + fileName;
            }

            var result = await _service.UpdateAsync(id, dto);

            if (!result)
                return NotFound();

            return RedirectToAction(
                nameof(Details),
                new { id });
        }
        catch (ValidationException ex)
        {
            foreach (var error in ex.Errors)
            {
                ModelState.AddModelError(
                    error.PropertyName,
                    error.ErrorMessage);
            }

            ViewBag.Properties = await _propertyService.GetAllAsync();
            return View(dto);
        }
        catch (KeyNotFoundException ex)
        {
            ModelState.AddModelError(
                nameof(dto.PropertyId),
                ex.Message);

            ViewBag.Properties = await _propertyService.GetAllAsync();
            return View(dto);
        }
    }

    // POST: /PropertyImage/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);

        if (!result)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }
}