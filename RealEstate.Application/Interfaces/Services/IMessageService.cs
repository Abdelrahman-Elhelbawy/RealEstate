using RealEstate.Application.DTOs.Message;

namespace RealEstate.Application.Interfaces;

public interface IMessageService
{
    Task<List<MessageListDto>> GetAllAsync();

    Task<MessageDto?> GetByIdAsync(int id);

    Task<List<MessageListDto>> GetByPropertyIdAsync(int propertyId);

    Task<int> CreateAsync(CreateMessageDto dto);

    Task<bool> UpdateAsync(int id, UpdateMessageDto dto);

    Task<bool> DeleteAsync(int id);

    Task<bool> MarkAsReadAsync(int id);
}