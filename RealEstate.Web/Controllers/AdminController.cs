using Microsoft.AspNetCore.Mvc;
using RealEstate.Application.Interfaces;
using RealEstate.Web.ViewModels;

namespace RealEstate.Web.Controllers;

public class AdminController : Controller
{
    private readonly IPropertyService _propertyService;
    private readonly IAgentService _agentService;
    private readonly IMessageService _messageService;
    private readonly IPropertyReportService _reportService;

    public AdminController(
        IPropertyService propertyService,
        IAgentService agentService,
        IMessageService messageService,
        IPropertyReportService reportService)
    {
        _propertyService = propertyService;
        _agentService = agentService;
        _messageService = messageService;
        _reportService = reportService;
    }

    // GET: /Admin
    // GET: /Admin/Index
    public async Task<IActionResult> Index()
    {
        var properties = await _propertyService.GetAllAsync();
        var agents = await _agentService.GetAllAsync();
        var messages = await _messageService.GetAllAsync();
        var reports = await _reportService.GetAllAsync();

        var viewModel = new AdminDashboardViewModel
        {
            TotalProperties = properties.Count,
            TotalAgents = agents.Count,
            TotalMessages = messages.Count,
            UnreadMessages = messages.Count(m => !m.IsRead),
            TotalReports = reports.Count,
            PendingReports = reports.Count(r => !r.IsResolved),

            RecentProperties = properties.OrderByDescending(p => p.Id).Take(5).ToList(),
            RecentMessages = messages.OrderByDescending(m => m.Id).Take(5).ToList(),
            RecentReports = reports.OrderByDescending(r => r.Id).Take(5).ToList()
        };

        return View(viewModel);
    }
}
