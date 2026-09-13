using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories;

public class PropertyImageRepository : IPropertyImageRepository
{
    private readonly AppDbContext _context;

    public PropertyImageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PropertyImage>> GetAllAsync()
    {
        return await _context.propertyImages
            .Include(x => x.property)
            .AsNoTracking()
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync();
    }

    public async Task<PropertyImage?> GetByIdAsync(int id)
    {
        return await _context.propertyImages
            .Include(x => x.property)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<PropertyImage>> GetByPropertyIdAsync(
        int propertyId)
    {
        return await _context.propertyImages
            .Where(x => x.propertyId == propertyId)
            .Include(x => x.property)
            .AsNoTracking()
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync();
    }

    public async Task AddAsync(PropertyImage propertyImage)
    {
        await _context.propertyImages.AddAsync(propertyImage);
    }

    public void Update(PropertyImage propertyImage)
    {
        _context.propertyImages.Update(propertyImage);
    }

    public void Delete(PropertyImage propertyImage)
    {
        _context.propertyImages.Remove(propertyImage);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.propertyImages
            .AnyAsync(x => x.Id == id);
    }

    public async Task<bool> PropertyExistsAsync(int propertyId)
    {
        return await _context.properties
            .AnyAsync(x => x.Id == propertyId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}