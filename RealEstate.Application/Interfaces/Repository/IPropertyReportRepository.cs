using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces;

public interface IPropertyReportRepository
{
    Task<List<PropertyReport>> GetAllAsync();

    Task<PropertyReport?> GetByIdAsync(int id);

    Task<List<PropertyReport>> GetByPropertyIdAsync(int propertyId);

    Task AddAsync(PropertyReport report);

    void Update(PropertyReport report);

    void Delete(PropertyReport report);

    Task<bool> ExistsAsync(int id);

    Task<bool> PropertyExistsAsync(int propertyId);

    Task SaveChangesAsync();
}