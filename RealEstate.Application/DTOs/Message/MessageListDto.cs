namespace RealEstate.Application.DTOs.Message;

public class MessageListDto
{
    public int Id { get; set; }

    public string? SenderName { get; set; }

    public string? SenderPhone { get; set; }

    public string? SenderEmail { get; set; }

    public string Content { get; set; } = string.Empty;

    public bool IsRead { get; set; }

    public int PropertyId { get; set; }

    public string? PropertyTitle { get; set; }
}