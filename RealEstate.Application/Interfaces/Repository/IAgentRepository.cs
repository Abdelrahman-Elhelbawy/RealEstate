using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces;

public interface IAgentRepository
{
    Task<List<Agent>> GetAllAsync();

    Task<Agent?> GetByIdAsync(int id);

    Task<Agent?> GetByIdWithPropertiesAsync(int id);

    Task AddAsync(Agent agent);

    void Update(Agent agent);

    void Delete(Agent agent);

    Task<bool> ExistsAsync(int id);

    Task<bool> HasPropertiesAsync(int id);

    Task SaveChangesAsync();
}