using Microsoft.EntityFrameworkCore;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;
using RealEstate.Infrastructure.Data;

namespace RealEstate.Infrastructure.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly AppDbContext _context;

    public MessageRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Message>> GetAllAsync()
    {
        return await _context.messages
            .Include(m => m.Property)
            .AsNoTracking()
            .OrderByDescending(m => m.Id)
            .ToListAsync();
    }

    public async Task<Message?> GetByIdAsync(int id)
    {
        return await _context.messages
            .Include(m => m.Property)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<List<Message>> GetByPropertyIdAsync(int propertyId)
    {
        return await _context.messages
            .Where(m => m.PropertyId == propertyId)
            .Include(m => m.Property)
            .AsNoTracking()
            .OrderByDescending(m => m.Id)
            .ToListAsync();
    }

    public async Task AddAsync(Message message)
    {
        await _context.messages.AddAsync(message);
    }

    public void Update(Message message)
    {
        _context.messages.Update(message);
    }

    public void Delete(Message message)
    {
        _context.messages.Remove(message);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.messages
            .AnyAsync(m => m.Id == id);
    }

    public async Task<bool> PropertyExistsAsync(int propertyId)
    {
        return await _context.properties
            .AnyAsync(p => p.Id == propertyId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}