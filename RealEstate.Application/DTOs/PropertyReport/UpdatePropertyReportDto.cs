using RealEstate.Domain.Enums;

namespace RealEstate.Application.DTOs.PropertyReport;

public class UpdatePropertyReportDto
{
    public ReportReason Reason { get; set; }

    public string? Description { get; set; }

    public string ReporterName { get; set; } = string.Empty;

    public string ReporterPhone { get; set; } = string.Empty;

    public bool IsResolved { get; set; }

    public int PropertyId { get; set; }
}