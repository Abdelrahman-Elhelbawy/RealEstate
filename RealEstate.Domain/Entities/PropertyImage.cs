namespace RealEstate.Domain.Entities;

public class PropertyImage
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public bool IsPrimary { get; set; }
    public int DisplayOrder { get; set; }

    // Relationships
    public int propertyId { get; set; }
    public Property property { get; set; }
}