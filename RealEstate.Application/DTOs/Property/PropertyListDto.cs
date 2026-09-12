using RealEstate.Domain.Enums;

namespace RealEstate.Application.DTOs.Property;

public class PropertyListDto
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public PropertyType PropertyType { get; set; }
    public TransactionType TransactionType { get; set; }

    public double Area { get; set; }

    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }

    public string? City { get; set; }

    public bool IsFeatured { get; set; }

    public int ViewsCount { get; set; }
}