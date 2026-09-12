namespace RealEstate.Domain.Entities;

public class Message
{
    public int Id { get; set; }
    public string? SenderName { get; set; } 
    public string? SenderPhone { get; set; } 
    public string? SenderEmail { get; set; }
    public string Content { get; set; }
    public bool IsRead { get; set; }

    // Relationships
    public int propertyId { get; set; }
    public Property property { get; set; }
}