namespace RealEstate.Application.DTOs.Agent;

public class AgentDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string WhatsApp { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public int PropertiesCount { get; set; }
}