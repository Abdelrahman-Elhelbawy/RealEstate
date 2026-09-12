using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces;

public interface IPropertyRepository
{
    Task<List<Property>> GetAllAsync();

    Task<Property?> GetByIdAsync(int id);

    Task AddAsync(Property property);

    void Update(Property property);

    void Delete(Property property);

    Task<bool> ExistsAsync(int id);

    Task SaveChangesAsync();
}