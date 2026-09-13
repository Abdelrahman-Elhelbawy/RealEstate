using RealEstate.Application.DTOs.PropertyImage;

namespace RealEstate.Application.Interfaces;

public interface IPropertyImageService
{
    Task<List<PropertyImageListDto>> GetAllAsync();

    Task<PropertyImageDto?> GetByIdAsync(int id);

    Task<List<PropertyImageListDto>> GetByPropertyIdAsync(int propertyId);

    Task<int> CreateAsync(CreatePropertyImageDto dto);

    Task<bool> UpdateAsync(
        int id,
        UpdatePropertyImageDto dto);

    Task<bool> DeleteAsync(int id);
}