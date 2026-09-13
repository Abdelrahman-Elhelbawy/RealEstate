using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.DTOs.Agent;
using RealEstate.Application.Interfaces;

namespace RealEstate.Web.Controllers;

public class AgentController : Controller
{
    private readonly IAgentService _agentService;

    public AgentController(IAgentService agentService)
    {
        _agentService = agentService;
    }

    // GET: /Agent
    public async Task<IActionResult> Index()
    {
        var agents = await _agentService.GetAllAsync();

        return View(agents);
    }

    // GET: /Agent/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var agent = await _agentService.GetByIdAsync(id);

        if (agent is null)
            return NotFound();

        return Json(agent);
    }

    // GET: /Agent/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: /Agent/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateAgentDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var agentId = await _agentService.CreateAsync(dto);

        return RedirectToAction(
            nameof(Details),
            new { id = agentId });
    }

    // GET: /Agent/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var agent = await _agentService.GetByIdAsync(id);

        if (agent is null)
            return NotFound();

        var dto = new UpdateAgentDto
        {
            Name = agent.Name,
            Phone = agent.Phone,
            WhatsApp = agent.WhatsApp,
            Email = agent.Email
        };

        return View(dto);
    }

    // POST: /Agent/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        UpdateAgentDto dto)
    {
        if (!ModelState.IsValid)
            return View(dto);

        var result = await _agentService.UpdateAsync(id, dto);

        if (!result)
            return NotFound();

        return RedirectToAction(
            nameof(Details),
            new { id });
    }

    // POST: /Agent/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _agentService.DeleteAsync(id);

        if (!result)
        {
            TempData["Error"] =
                "Agent cannot be deleted because it has properties.";

            return RedirectToAction(nameof(Index));
        }

        return RedirectToAction(nameof(Index));
    }
}