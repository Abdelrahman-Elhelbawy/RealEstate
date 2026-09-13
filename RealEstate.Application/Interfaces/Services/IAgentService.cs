using RealEstate.Application.DTOs.Agent;

namespace RealEstate.Application.Interfaces;

public interface IAgentService
{
    Task<List<AgentListDto>> GetAllAsync();

    Task<AgentDto?> GetByIdAsync(int id);

    Task<AgentDto?> GetByIdWithPropertiesAsync(int id);

    Task<int> CreateAsync(CreateAgentDto dto);

    Task<bool> UpdateAsync(int id, UpdateAgentDto dto);

    Task<bool> DeleteAsync(int id);
}