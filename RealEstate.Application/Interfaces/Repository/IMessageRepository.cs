using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces;

public interface IMessageRepository
{
    Task<List<Message>> GetAllAsync();

    Task<Message?> GetByIdAsync(int id);

    Task<List<Message>> GetByPropertyIdAsync(int propertyId);

    Task AddAsync(Message message);

    void Update(Message message);

    void Delete(Message message);

    Task<bool> ExistsAsync(int id);

    Task<bool> PropertyExistsAsync(int propertyId);

    Task SaveChangesAsync();
}