namespace RealEstate.Application.DTOs.PropertyImage;

public class PropertyImageListDto
{
    public int Id { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public bool IsPrimary { get; set; }

    public int DisplayOrder { get; set; }

    public int PropertyId { get; set; }

    public string? PropertyTitle { get; set; }
}