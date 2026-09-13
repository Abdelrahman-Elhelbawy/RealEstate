namespace RealEstate.Application.DTOs.PropertyImage;

public class UpdatePropertyImageDto
{
    public string ImageUrl { get; set; } = string.Empty;

    public bool IsPrimary { get; set; }

    public int DisplayOrder { get; set; }

    public int PropertyId { get; set; }
}