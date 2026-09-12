using RealEstate.Domain.Enums;
namespace RealEstate.Domain.Entities;

public class Property
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public PropertyType PropertyType { get; set; }
    public TransactionType TransactionType { get; set; }
    public double Area { get; set; }
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public int Floor { get; set; }
    public bool Furnished { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; } 
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public PropertyStatus Status { get; set; }
    public bool IsFeatured { get; set; }
    public int ViewsCount { get; set; }

    // Relationships
    public int agentId { get; set; }
    public Agent agent { get; set; }

    public List<Message> messages { get; set; }
    public List<PropertyImage> propertyImages { get; set; }
    public List<PropertyReport> propertyReports { get; set; }
}