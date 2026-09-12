using RealEstate.Application.DTOs.Property;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Services;

public class PropertyService : IPropertyService
{
    private readonly IPropertyRepository _propertyRepository;

    public PropertyService(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<List<PropertyListDto>> GetAllAsync()
    {
        var properties = await _propertyRepository.GetAllAsync();

        return properties.Select(p => new PropertyListDto
        {
            Id = p.Id,
            Title = p.Title,
            Price = p.Price,
            PropertyType = p.PropertyType,
            TransactionType = p.TransactionType,
            Area = p.Area,
            Bedrooms = p.Bedrooms,
            Bathrooms = p.Bathrooms,
            City = p.City,
            IsFeatured = p.IsFeatured,
            ViewsCount = p.ViewsCount
        }).ToList();
    }

    public async Task<PropertyDto?> GetByIdAsync(int id)
    {
        var property = await _propertyRepository.GetByIdAsync(id);

        if (property == null)
            return null;

        return new PropertyDto
        {
            Id = property.Id,
            Title = property.Title,
            Description = property.Description,
            Price = property.Price,
            PropertyType = property.PropertyType,
            TransactionType = property.TransactionType,
            Area = property.Area,
            Bedrooms = property.Bedrooms,
            Bathrooms = property.Bathrooms,
            Floor = property.Floor,
            Furnished = property.Furnished,
            Address = property.Address,
            City = property.City,
            Latitude = property.Latitude,
            Longitude = property.Longitude,
            Status = property.Status,
            IsFeatured = property.IsFeatured,
            ViewsCount = property.ViewsCount,
            AgentId = property.agentId
        };
    }

    public async Task<int> CreateAsync(CreatePropertyDto dto)
    {
        var property = new Property
        {
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            PropertyType = dto.PropertyType,
            TransactionType = dto.TransactionType,
            Area = dto.Area,
            Bedrooms = dto.Bedrooms,
            Bathrooms = dto.Bathrooms,
            Floor = dto.Floor,
            Furnished = dto.Furnished,
            Address = dto.Address,
            City = dto.City,
            Latitude = dto.Latitude,
            Longitude = dto.Longitude,
            agentId = dto.AgentId,
            Status = Domain.Enums.PropertyStatus.Pending,
            IsFeatured = false,
            ViewsCount = 0
        };

        await _propertyRepository.AddAsync(property);
        await _propertyRepository.SaveChangesAsync();

        return property.Id;
    }

    public async Task<bool> UpdateAsync(int id, UpdatePropertyDto dto)
    {
        var property = await _propertyRepository.GetByIdAsync(id);

        if (property == null)
            return false;

        property.Title = dto.Title;
        property.Description = dto.Description;
        property.Price = dto.Price;
        property.PropertyType = dto.PropertyType;
        property.TransactionType = dto.TransactionType;
        property.Area = dto.Area;
        property.Bedrooms = dto.Bedrooms;
        property.Bathrooms = dto.Bathrooms;
        property.Floor = dto.Floor;
        property.Furnished = dto.Furnished;
        property.Address = dto.Address;
        property.City = dto.City;
        property.Latitude = dto.Latitude;
        property.Longitude = dto.Longitude;
        property.agentId = dto.AgentId;

        _propertyRepository.Update(property);
        await _propertyRepository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var property = await _propertyRepository.GetByIdAsync(id);

        if (property == null)
            return false;

        _propertyRepository.Delete(property);
        await _propertyRepository.SaveChangesAsync();

        return true;
    }
}