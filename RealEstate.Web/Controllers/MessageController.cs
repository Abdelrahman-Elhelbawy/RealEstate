using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs.Message;
using RealEstate.Application.Interfaces;

namespace RealEstate.Web.Controllers;

public class MessageController : Controller
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    // GET: /Message
    public async Task<IActionResult> Index()
    {
        var messages = await _messageService.GetAllAsync();

        return View(messages);
    }

    // GET: /Message/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var message = await _messageService.GetByIdAsync(id);

        if (message is null)
            return NotFound();

        return View(message);
    }

    // GET: /Message/Create
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Message/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        CreateMessageDto dto)
    {
        try
        {
            var id = await _messageService.CreateAsync(dto);

            return RedirectToAction(nameof(Details), new { id });
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

    // GET: /Message/Edit/5
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var message = await _messageService.GetByIdAsync(id);

        if (message is null)
            return NotFound();

        var dto = new UpdateMessageDto
        {
            SenderName = message.SenderName,
            SenderPhone = message.SenderPhone,
            SenderEmail = message.SenderEmail,
            Content = message.Content,
            IsRead = message.IsRead,
            PropertyId = message.PropertyId
        };

        return View(dto);
    }

    // POST: /Message/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        UpdateMessageDto dto)
    {
        try
        {
            var result =
                await _messageService.UpdateAsync(id, dto);

            if (!result)
                return NotFound();

            return RedirectToAction(nameof(Details), new { id });
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

    // POST: /Message/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _messageService.DeleteAsync(id);

        if (!result)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }

    // POST: /Message/MarkAsRead/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        var result =
            await _messageService.MarkAsReadAsync(id);

        if (!result)
            return NotFound();

        return RedirectToAction(nameof(Index));
    }
}