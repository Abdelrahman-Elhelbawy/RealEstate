using FluentValidation;
using RealEstate.Application.DTOs.PropertyImage;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Services;

public class PropertyImageService : IPropertyImageService
{
    private readonly IPropertyImageRepository _repository;

    private readonly IValidator<CreatePropertyImageDto>
        _createValidator;

    private readonly IValidator<UpdatePropertyImageDto>
        _updateValidator;

    public PropertyImageService(
        IPropertyImageRepository repository,
        IValidator<CreatePropertyImageDto> createValidator,
        IValidator<UpdatePropertyImageDto> updateValidator)
    {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<List<PropertyImageListDto>> GetAllAsync()
    {
        var images = await _repository.GetAllAsync();

        return images.Select(x => new PropertyImageListDto
        {
            Id = x.Id,
            ImageUrl = x.ImageUrl,
            IsPrimary = x.IsPrimary,
            DisplayOrder = x.DisplayOrder,
            PropertyId = x.propertyId,
            PropertyTitle = x.property?.Title
        }).ToList();
    }

    public async Task<PropertyImageDto?> GetByIdAsync(int id)
    {
        var image = await _repository.GetByIdAsync(id);

        if (image is null)
            return null;

        return new PropertyImageDto
        {
            Id = image.Id,
            ImageUrl = image.ImageUrl,
            IsPrimary = image.IsPrimary,
            DisplayOrder = image.DisplayOrder,
            PropertyId = image.propertyId,
            PropertyTitle = image.property?.Title
        };
    }

    public async Task<List<PropertyImageListDto>>
        GetByPropertyIdAsync(int propertyId)
    {
        var images =
            await _repository.GetByPropertyIdAsync(propertyId);

        return images.Select(x => new PropertyImageListDto
        {
            Id = x.Id,
            ImageUrl = x.ImageUrl,
            IsPrimary = x.IsPrimary,
            DisplayOrder = x.DisplayOrder,
            PropertyId = x.propertyId,
            PropertyTitle = x.property?.Title
        }).ToList();
    }

    public async Task<int> CreateAsync(
        CreatePropertyImageDto dto)
    {
        var validationResult =
            await _createValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            throw new ValidationException(
                validationResult.Errors);

        var propertyExists =
            await _repository.PropertyExistsAsync(
                dto.PropertyId);

        if (!propertyExists)
            throw new KeyNotFoundException(
                $"Property with id {dto.PropertyId} does not exist.");

        var image = new PropertyImage
        {
            ImageUrl = dto.ImageUrl,
            IsPrimary = dto.IsPrimary,
            DisplayOrder = dto.DisplayOrder,
            propertyId = dto.PropertyId
        };

        await _repository.AddAsync(image);

        await _repository.SaveChangesAsync();

        return image.Id;
    }

    public async Task<bool> UpdateAsync(
        int id,
        UpdatePropertyImageDto dto)
    {
        var validationResult =
            await _updateValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            throw new ValidationException(
                validationResult.Errors);

        var image =
            await _repository.GetByIdAsync(id);

        if (image is null)
            return false;

        var propertyExists =
            await _repository.PropertyExistsAsync(
                dto.PropertyId);

        if (!propertyExists)
            throw new KeyNotFoundException(
                $"Property with id {dto.PropertyId} does not exist.");

        image.ImageUrl = dto.ImageUrl;
        image.IsPrimary = dto.IsPrimary;
        image.DisplayOrder = dto.DisplayOrder;
        image.propertyId = dto.PropertyId;

        _repository.Update(image);

        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var image =
            await _repository.GetByIdAsync(id);

        if (image is null)
            return false;

        _repository.Delete(image);

        await _repository.SaveChangesAsync();

        return true;
    }
}