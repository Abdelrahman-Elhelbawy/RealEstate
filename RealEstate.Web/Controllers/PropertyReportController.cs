using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs.PropertyReport;
using RealEstate.Application.Interfaces;

namespace RealEstate.Web.Controllers;

public class PropertyReportController : Controller
{
    private readonly IPropertyReportService _service;

    public PropertyReportController(
        IPropertyReportService service)
    {
        _service = service;
    }

    // GET: /PropertyReport
    public async Task<IActionResult> Index()
    {
        var reports = await _service.GetAllAsync();

        return View(reports);
    }

    // GET: /PropertyReport/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var report = await _service.GetByIdAsync(id);

        if (report is null)
            return NotFound();

        return View(report);
    }

    // GET: /PropertyReport/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: /PropertyReport/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CreatePropertyReportDto dto)
    {
        try
        {
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

            return View(dto);
        }
        catch (KeyNotFoundException ex)
        {
            ModelState.AddModelError(
                nameof(dto.PropertyId),
                ex.Message);

            return View(dto);
        }
    }

    // GET: /PropertyReport/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var report = await _service.GetByIdAsync(id);

        if (report is null)
            return NotFound();

        var dto = new UpdatePropertyReportDto
        {
            Reason = report.Reason,
            Description = report.Description,
            ReporterName = report.ReporterName,
            ReporterPhone = report.ReporterPhone,
            IsResolved = report.IsResolved,
            PropertyId = report.PropertyId
        };

        return View(dto);
    }

    // POST: /PropertyReport/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        UpdatePropertyReportDto dto)
    {
        try
        {
            var result =
                await _service.UpdateAsync(id, dto);

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

            return View(dto);
        }
        catch (KeyNotFoundException ex)
        {
            ModelState.AddModelError(
                nameof(dto.PropertyId),
                ex.Message);

            return View(dto);
        }
    }

    // POST: /PropertyReport/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);

        if (!result)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }

    // POST: /PropertyReport/Resolve/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Resolve(int id)
    {
        var result = await _service.ResolveAsync(id);

        if (!result)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }
}