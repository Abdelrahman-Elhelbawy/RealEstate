using RealEstate.Domain.Entities;

namespace RealEstate.Application.Interfaces;

public interface IPropertyImageRepository
{
    Task<List<PropertyImage>> GetAllAsync();

    Task<PropertyImage?> GetByIdAsync(int id);

    Task<List<PropertyImage>> GetByPropertyIdAsync(int propertyId);

    Task AddAsync(PropertyImage propertyImage);

    void Update(PropertyImage propertyImage);

    void Delete(PropertyImage propertyImage);

    Task<bool> ExistsAsync(int id);

    Task<bool> PropertyExistsAsync(int propertyId);

    Task SaveChangesAsync();
}