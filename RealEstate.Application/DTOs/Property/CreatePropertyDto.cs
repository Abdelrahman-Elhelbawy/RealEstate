using RealEstate.Domain.Enums;

namespace RealEstate.Application.DTOs.Property;

public class CreatePropertyDto
{
    public string Title { get; set; } = string.Empty;
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

    public int AgentId { get; set; }
}