using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories;

public class AgentRepository : IAgentRepository
{
    private readonly AppDbContext _context;

    public AgentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Agent>> GetAllAsync()
    {
        return await _context.agents
            .Include(a => a.properties)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Agent?> GetByIdAsync(int id)
    {
        return await _context.agents
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Agent?> GetByIdWithPropertiesAsync(int id)
    {
        return await _context.agents
            .Include(a => a.properties)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task AddAsync(Agent agent)
    {
        await _context.agents.AddAsync(agent);
    }

    public void Update(Agent agent)
    {
        _context.agents.Update(agent);
    }

    public void Delete(Agent agent)
    {
        _context.agents.Remove(agent);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.agents
            .AnyAsync(a => a.Id == id);
    }

    public async Task<bool> HasPropertiesAsync(int id)
    {
        return await _context.properties
            .AnyAsync(p => p.agentId == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}