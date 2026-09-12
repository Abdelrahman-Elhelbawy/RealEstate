using RealEstate.Domain.Enums;

namespace RealEstate.Domain.Entities
{
    public class PropertyReport
    {
        public int Id { get; set; }

        public ReportReason Reason { get; set; }

        public string? Description { get; set; }

        public string ReporterName { get; set; } = string.Empty;

        public string ReporterPhone { get; set; } = string.Empty;

        public bool IsResolved { get; set; }

        public DateTime CreatedAt { get; set; }

        // Relationships
        public int propertyId { get; set; }
        public Property property { get; set; }
    }
}