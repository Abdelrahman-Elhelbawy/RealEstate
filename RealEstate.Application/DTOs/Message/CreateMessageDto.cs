namespace RealEstate.Application.DTOs.Message;

public class CreateMessageDto
{
    public string? SenderName { get; set; }

    public string? SenderPhone { get; set; }

    public string? SenderEmail { get; set; }

    public string Content { get; set; } = string.Empty;

    public int PropertyId { get; set; }
}