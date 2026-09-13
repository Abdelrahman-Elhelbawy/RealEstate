using FluentValidation;
using RealEstate.Application.DTOs.PropertyReport;
using RealEstate.Application.Interfaces;
using RealEstate.Domain.Entities;

namespace RealEstate.Application.Services;

public class PropertyReportService : IPropertyReportService
{
    private readonly IPropertyReportRepository _repository;

    private readonly IValidator<CreatePropertyReportDto>
        _createValidator;

    private readonly IValidator<UpdatePropertyReportDto>
        _updateValidator;

    public PropertyReportService(
        IPropertyReportRepository repository,
        IValidator<CreatePropertyReportDto> createValidator,
        IValidator<UpdatePropertyReportDto> updateValidator)
    {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<List<PropertyReportListDto>> GetAllAsync()
    {
        var reports = await _repository.GetAllAsync();

        return reports.Select(x => new PropertyReportListDto
        {
            Id = x.Id,
            Reason = x.Reason,
            Description = x.Description,
            ReporterName = x.ReporterName,
            ReporterPhone = x.ReporterPhone,
            IsResolved = x.IsResolved,
            CreatedAt = x.CreatedAt,
            PropertyId = x.propertyId,
            PropertyTitle = x.property?.Title
        }).ToList();
    }

    public async Task<PropertyReportDto?> GetByIdAsync(int id)
    {
        var report = await _repository.GetByIdAsync(id);

        if (report is null)
            return null;

        return new PropertyReportDto
        {
            Id = report.Id,
            Reason = report.Reason,
            Description = report.Description,
            ReporterName = report.ReporterName,
            ReporterPhone = report.ReporterPhone,
            IsResolved = report.IsResolved,
            CreatedAt = report.CreatedAt,
            PropertyId = report.propertyId,
            PropertyTitle = report.property?.Title
        };
    }

    public async Task<List<PropertyReportListDto>>
        GetByPropertyIdAsync(int propertyId)
    {
        var reports =
            await _repository.GetByPropertyIdAsync(propertyId);

        return reports.Select(x => new PropertyReportListDto
        {
            Id = x.Id,
            Reason = x.Reason,
            Description = x.Description,
            ReporterName = x.ReporterName,
            ReporterPhone = x.ReporterPhone,
            IsResolved = x.IsResolved,
            CreatedAt = x.CreatedAt,
            PropertyId = x.propertyId,
            PropertyTitle = x.property?.Title
        }).ToList();
    }

    public async Task<int> CreateAsync(
        CreatePropertyReportDto dto)
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
        {
            throw new KeyNotFoundException(
                $"Property with id {dto.PropertyId} does not exist.");
        }

        var report = new PropertyReport
        {
            Reason = dto.Reason,
            Description = dto.Description,
            ReporterName = dto.ReporterName,
            ReporterPhone = dto.ReporterPhone,
            IsResolved = false,
            CreatedAt = DateTime.UtcNow,
            propertyId = dto.PropertyId
        };

        await _repository.AddAsync(report);

        await _repository.SaveChangesAsync();

        return report.Id;
    }

    public async Task<bool> UpdateAsync(
        int id,
        UpdatePropertyReportDto dto)
    {
        var validationResult =
            await _updateValidator.ValidateAsync(dto);

        if (!validationResult.IsValid)
            throw new ValidationException(
                validationResult.Errors);

        var report =
            await _repository.GetByIdAsync(id);

        if (report is null)
            return false;

        var propertyExists =
            await _repository.PropertyExistsAsync(
                dto.PropertyId);

        if (!propertyExists)
        {
            throw new KeyNotFoundException(
                $"Property with id {dto.PropertyId} does not exist.");
        }

        report.Reason = dto.Reason;
        report.Description = dto.Description;
        report.ReporterName = dto.ReporterName;
        report.ReporterPhone = dto.ReporterPhone;
        report.IsResolved = dto.IsResolved;
        report.propertyId = dto.PropertyId;

        _repository.Update(report);

        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var report =
            await _repository.GetByIdAsync(id);

        if (report is null)
            return false;

        _repository.Delete(report);

        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> ResolveAsync(int id)
    {
        var report =
            await _repository.GetByIdAsync(id);

        if (report is null)
            return false;

        report.IsResolved = true;

        _repository.Update(report);

        await _repository.SaveChangesAsync();

        return true;
    }
}