using RealEstate.Domain.Enums;

namespace RealEstate.Application.DTOs.PropertyReport;

public class PropertyReportDto
{
    public int Id { get; set; }

    public ReportReason Reason { get; set; }

    public string? Description { get; set; }

    public string ReporterName { get; set; } = string.Empty;

    public string ReporterPhone { get; set; } = string.Empty;

    public bool IsResolved { get; set; }

    public DateTime CreatedAt { get; set; }

    public int PropertyId { get; set; }

    public string? PropertyTitle { get; set; }
}