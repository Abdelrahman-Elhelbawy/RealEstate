using RealEstate.Application.DTOs.Property;

namespace RealEstate.Application.Interfaces;

public interface IPropertyService
{
    Task<List<PropertyListDto>> GetAllAsync();

    Task<PropertyDto?> GetByIdAsync(int id);

    Task<int> CreateAsync(CreatePropertyDto dto);

    Task<bool> UpdateAsync(int id, UpdatePropertyDto dto);

    Task<bool> DeleteAsync(int id);
}