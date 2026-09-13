using RealEstate.Application.DTOs.PropertyReport;

namespace RealEstate.Application.Interfaces;

public interface IPropertyReportService
{
    Task<List<PropertyReportListDto>> GetAllAsync();

    Task<PropertyReportDto?> GetByIdAsync(int id);

    Task<List<PropertyReportListDto>> GetByPropertyIdAsync(
        int propertyId);

    Task<int> CreateAsync(CreatePropertyReportDto dto);

    Task<bool> UpdateAsync(
        int id,
        UpdatePropertyReportDto dto);

    Task<bool> DeleteAsync(int id);

    Task<bool> ResolveAsync(int id);
}