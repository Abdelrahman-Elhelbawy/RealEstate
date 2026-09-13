using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories;

public class PropertyReportRepository : IPropertyReportRepository
{
    private readonly AppDbContext _context;

    public PropertyReportRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<PropertyReport>> GetAllAsync()
    {
        return await _context.propertyReports
            .Include(x => x.property)
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<PropertyReport?> GetByIdAsync(int id)
    {
        return await _context.propertyReports
            .Include(x => x.property)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<PropertyReport>> GetByPropertyIdAsync(
        int propertyId)
    {
        return await _context.propertyReports
            .Where(x => x.propertyId == propertyId)
            .Include(x => x.property)
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task AddAsync(PropertyReport report)
    {
        await _context.propertyReports.AddAsync(report);
    }

    public void Update(PropertyReport report)
    {
        _context.propertyReports.Update(report);
    }

    public void Delete(PropertyReport report)
    {
        _context.propertyReports.Remove(report);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.propertyReports
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