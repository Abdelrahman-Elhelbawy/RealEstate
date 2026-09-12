using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly AppDbContext _context;

    public PropertyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Property>> GetAllAsync()
    {
        return await _context.properties
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Property?> GetByIdAsync(int id)
    {
        return await _context.properties
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Property property)
    {
        await _context.properties.AddAsync(property);
    }

    public void Update(Property property)
    {
        _context.properties.Update(property);
    }

    public void Delete(Property property)
    {
        _context.properties.Remove(property);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.properties
            .AnyAsync(p => p.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}