using RealEstate.Application.DTOs.Agent;
using RealEstate.Application.DTOs.Message;
using RealEstate.Application.DTOs.Property;
using RealEstate.Application.DTOs.PropertyReport;

namespace RealEstate.Web.ViewModels;

public class AdminDashboardViewModel
{
    public int TotalProperties { get; set; }
    public int TotalAgents { get; set; }
    public int TotalMessages { get; set; }
    public int UnreadMessages { get; set; }
    public int TotalReports { get; set; }
    public int PendingReports { get; set; }

    public List<PropertyListDto> RecentProperties { get; set; } = new();
    public List<MessageListDto> RecentMessages { get; set; } = new();
    public List<PropertyReportListDto> RecentReports { get; set; } = new();
}
